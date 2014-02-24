using RWEST.Nexus.MDM.Contracts;
using RWEST.Nexus.MDM.Extensions;
using RWEST.Nexus.Search;

namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Linq;
    using RWEST.Nexus.Contracts.Search;
    using RWEST.Nexus.Data.EntityFramework;

    public static class CurveData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static CurveData()
        {
            repository = ObjectScript.Repository;
        }

        public static Curve CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Curve>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Curve CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Curve>();

            var endurMapping = new CurveMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = endur, 
                    IsDefault = true, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue.Subtract(new TimeSpan(72, 0, 0)))
                };

            entity.ProcessMapping(endurMapping);
            repository.Add(entity);
            repository.Flush();

            return entity;
        }

        public static Contracts.Curve CreateContractForEntityCreation()
        {
            var contract = new Contracts.Curve();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Curve CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Curve();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new CurveMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new CurveMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = endur, 
                    IsDefault = true, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            entity.ProcessMapping(trayportMapping);
            entity.ProcessMapping(endurMapping);

            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static void CreateSearch(Search search, Curve entity1, Curve entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static Contracts.Curve MakeChangeToContract(Contracts.Curve currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(Contracts.Curve contract)
        {
			// TODO_IntegrationTestGeneration_Curve - Add details to a contract
            var curve = ObjectMother.Create<Curve>();
            repository.Add(curve);
            repository.Flush();

            contract.Details = new CurveDetails()
                {
                    Name = curve.Name, 
                    Commodity = curve.Commodity.CreateNexusEntityId(() => curve.Commodity.Name), 
                    CommodityUnit = curve.CommodityUnit,
                    Currency = curve.Currency, 
                    DefaultSpread = curve.DefaultSpread,
                    Location = curve.Location.CreateNexusEntityId(() => curve.Location.Name), 
                    CurveType = curve.Type,
                    Originator = curve.Originator.CreateNexusEntityId(() => curve.Originator.LatestDetails.Name), 
                };
        }

        private static void AddDetailsToEntity(Curve entity, DateTime startDate, DateTime endDate)
        {
			// TODO_IntegrationTestGeneration_Curve - Add details to an entity

            var newEntity = ObjectMother.Create<Curve>();
            entity.Name = newEntity.Name;
            entity.Type = newEntity.Type;
            entity.Commodity = newEntity.Commodity;
            entity.CommodityUnit= newEntity.CommodityUnit;
            entity.Location = newEntity.Location;
            entity.Originator = newEntity.Originator;
            entity.Currency = newEntity.Currency;
            entity.DefaultSpread = newEntity.DefaultSpread;
        }

        private static void CreateSearchData(Search search, Curve entity1, Curve entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
