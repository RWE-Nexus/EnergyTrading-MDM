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
    using SourceSystem = EnergyTrading.MDM.SourceSystem;
    using Unit = EnergyTrading.MDM.Unit;

    public static class UnitData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static UnitData()
        {
            repository = ObjectScript.Repository;
        }

        public static Unit CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Unit>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Unit CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Unit>();

            var endurMapping = new UnitMapping
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

        public static RWEST.Nexus.MDM.Contracts.Unit CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Unit();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Unit CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Unit();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new UnitMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new UnitMapping
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

        public static void CreateSearch(Search search, Unit entity1, Unit entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Unit MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Unit currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Unit contract)
        {
			var entity = ObjectMother.Create<Unit>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new UnitDetails()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    ConversionConstant = entity.ConversionConstant,
                    ConversionFactor = entity.ConversionFactor,
                    Symbol = entity.Symbol,
                    Dimension = entity.Dimension.CreateNexusEntityId(() => entity.Dimension.Name)
                };


            // delete entity as we will attempt to post this exact contract and we may violate integrity constraints
            // the purpose of the persistence is to save the related object graph
            repository.Delete(entity);
            repository.Flush();
        }

        private static void AddDetailsToEntity(Unit entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<Unit>();
            entity.Name = newEntity.Name;
            entity.ConversionConstant = newEntity.ConversionConstant;
            entity.ConversionFactor = newEntity.ConversionFactor;
            entity.Description = newEntity.Description;
            entity.Symbol = newEntity.Symbol;
            entity.Dimension = newEntity.Dimension;
        }

        private static void CreateSearchData(Search search, Unit entity1, Unit entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
