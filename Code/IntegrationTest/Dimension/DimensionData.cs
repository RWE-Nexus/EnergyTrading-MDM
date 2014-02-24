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
    using Dimension = EnergyTrading.MDM.Dimension;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class DimensionData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static DimensionData()
        {
            repository = ObjectScript.Repository;
        }

        public static Dimension CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Dimension>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Dimension CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Dimension>();

            var endurMapping = new DimensionMapping
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

        public static RWEST.Nexus.MDM.Contracts.Dimension CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Dimension();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Dimension CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Dimension();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new DimensionMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new DimensionMapping
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

        public static void CreateSearch(Search search, Dimension entity1, Dimension entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Dimension MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Dimension currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Dimension contract)
        {
            var entity = ObjectMother.Create<Dimension>();
            
            contract.Details = new DimensionDetails()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    ElectricCurrentDimension = entity.ElectricCurrentDimension,
                    LengthDimension = entity.LengthDimension,
                    LuminousIntensityDimension = entity.LuminousIntensityDimension,
                    TemperatureDimension = entity.TemperatureDimension,
                    TimeDimension = entity.TimeDimension,
                    MassDimension = entity.MassDimension
                };
        }

        private static void AddDetailsToEntity(Dimension entity, DateTime startDate, DateTime endDate)
        {
			var newEntity = ObjectMother.Create<Dimension>();
            entity.Name = newEntity.Name;
            entity.Description = newEntity.Description;
            entity.ElectricCurrentDimension = newEntity.ElectricCurrentDimension;
            entity.LengthDimension = newEntity.LengthDimension;
            entity.LuminousIntensityDimension = newEntity.LuminousIntensityDimension;
            entity.TimeDimension = newEntity.TimeDimension;
            entity.TemperatureDimension = newEntity.TemperatureDimension;
            entity.MassDimension = newEntity.MassDimension;
        }

        private static void CreateSearchData(Search search, Dimension entity1, Dimension entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
