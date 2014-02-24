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
    using TenorType = EnergyTrading.MDM.TenorType;

    public static class TenorTypeData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static TenorTypeData()
        {
            repository = ObjectScript.Repository;
        }

        public static TenorType CreateBasicEntity()
        {
            var entity = ObjectMother.Create<TenorType>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static TenorType CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<TenorType>();

            var endurMapping = new TenorTypeMapping
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

        public static RWEST.Nexus.MDM.Contracts.TenorType CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.TenorType();
            AddDetailsToContract(contract);
            return contract;
        }

        public static TenorType CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new TenorType();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new TenorTypeMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new TenorTypeMapping
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

        public static void CreateSearch(Search search, TenorType entity1, TenorType entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.TenorType MakeChangeToContract(RWEST.Nexus.MDM.Contracts.TenorType currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.TenorType contract)
        {
            var entity = ObjectMother.Create<TenorType>();

            contract.Details = new TenorTypeDetails
            {
                Name = entity.Name,
                ShortName = entity.ShortName,
                //Traded = entity.Traded.ToContract(),
            };
        }

        private static void AddDetailsToEntity(TenorType entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<TenorType>();
            entity.Name = newEntity.Name;
            entity.ShortName = newEntity.ShortName;
            //entity.Traded = newEntity.Traded;
        }

        private static void CreateSearchData(Search search, TenorType entity1, TenorType entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
