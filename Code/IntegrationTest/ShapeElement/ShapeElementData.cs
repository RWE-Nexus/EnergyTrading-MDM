namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    using DateRange = EnergyTrading.DateRange;
    using ShapeElement = EnergyTrading.MDM.ShapeElement;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class ShapeElementData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static ShapeElementData()
        {
            repository = ObjectScript.Repository;
        }

        public static ShapeElement CreateBasicEntity()
        {
            var entity = ObjectMother.Create<ShapeElement>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static ShapeElement CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<ShapeElement>();

            var endurMapping = new ShapeElementMapping
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

        public static RWEST.Nexus.MDM.Contracts.ShapeElement CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.ShapeElement();
            AddDetailsToContract(contract);
            return contract;
        }

        public static ShapeElement CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new ShapeElement();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new ShapeElementMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new ShapeElementMapping
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

        public static void CreateSearch(Search search, ShapeElement entity1, ShapeElement entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.ShapeElement MakeChangeToContract(RWEST.Nexus.MDM.Contracts.ShapeElement currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ShapeElement contract)
        {
            var entity = ObjectMother.Create<ShapeElement>();

            contract.Details = new ShapeElementDetails()
            {
                Name = entity.Name,
                Period = entity.Period.ToContract()
            };
        }

        private static void AddDetailsToEntity(ShapeElement entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<ShapeElement>();
            entity.Name = newEntity.Name;
            entity.Period = newEntity.Period;
        }

        private static void CreateSearchData(Search search, ShapeElement entity1, ShapeElement entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
