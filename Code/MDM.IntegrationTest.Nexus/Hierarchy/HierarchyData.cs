namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;
    using DateRange = EnergyTrading.DateRange;
    using Hierarchy = EnergyTrading.MDM.Hierarchy;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class HierarchyData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static HierarchyData()
        {
            repository = ObjectScript.Repository;
        }

        public static Hierarchy CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Hierarchy>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Hierarchy CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Hierarchy>();

            var endurMapping = new HierarchyMapping
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

        public static RWEST.Nexus.MDM.Contracts.Hierarchy CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Hierarchy();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Hierarchy CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Hierarchy();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new HierarchyMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new HierarchyMapping
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

        public static void CreateSearch(Search search, Hierarchy entity1, Hierarchy entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Hierarchy MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Hierarchy currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Hierarchy contract)
        {
            var entity = ObjectMother.Create<Hierarchy>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new HierarchyDetails()
                {
                    Name = entity.Name,
                };
        }

        private static void AddDetailsToEntity(Hierarchy entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<Hierarchy>();
            entity.Name = newEntity.Name;
        }

        private static void CreateSearchData(Search search, Hierarchy entity1, Hierarchy entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
