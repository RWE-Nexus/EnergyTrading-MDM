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
    using Shape = EnergyTrading.MDM.Shape;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class ShapeData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static ShapeData()
        {
            repository = ObjectScript.Repository;
        }

        public static Shape CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Shape>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Shape CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Shape>();

            var endurMapping = new ShapeMapping
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

        public static RWEST.Nexus.MDM.Contracts.Shape CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Shape();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Shape CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Shape();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new ShapeMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new ShapeMapping
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

        public static void CreateSearch(Search search, Shape entity1, Shape entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Shape MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Shape currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Shape contract)
        {
            contract.Details = new ShapeDetails()
            {
                Name = "Shape" + Guid.NewGuid()
            };
        }

        private static void AddDetailsToEntity(Shape entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<Shape>();
            entity.Name = newEntity.Name;
        }

        private static void CreateSearchData(Search search, Shape entity1, Shape entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
