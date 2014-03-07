using RWEST.Nexus.MDM.Contracts;
using EnergyTrading.MDM.Extensions;
using EnergyTrading.Search;

namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;

    public static class VesselData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static VesselData()
        {
            repository = ObjectScript.Repository;
        }

        public static Vessel CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Vessel>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Vessel CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Vessel>();

            var endurMapping = new VesselMapping
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

        public static RWEST.Nexus.MDM.Contracts.Vessel CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Vessel();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Vessel CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Vessel();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new VesselMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new VesselMapping
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

        public static void CreateSearch(Search search, Vessel entity1, Vessel entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Vessel MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Vessel currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Vessel contract)
        {
			
			
            var entity = ObjectMother.Create<Vessel>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new VesselDetails()
                {
                    Name = entity.Name,
                };
			
        }

        private static void AddDetailsToEntity(Vessel entity, DateTime startDate, DateTime endDate)
        {
			var newEntity = ObjectMother.Create<Vessel>();
            entity.Name = newEntity.Name;
        }

        private static void CreateSearchData(Search search, Vessel entity1, Vessel entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
