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
    using ProductTenorType = EnergyTrading.MDM.ProductTenorType;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;
    using TenorType = EnergyTrading.MDM.TenorType;

    public static class ProductTenorTypeData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static ProductTenorTypeData()
        {
            repository = ObjectScript.Repository;
        }

        public static ProductTenorType CreateBasicEntity()
        {
            var entity = ObjectMother.Create<ProductTenorType>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static ProductTenorType CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<ProductTenorType>();

            var endurMapping = new ProductTenorTypeMapping
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

        public static RWEST.Nexus.MDM.Contracts.ProductTenorType CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.ProductTenorType();
            AddDetailsToContract(contract);
            return contract;
        }

        public static ProductTenorType CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new ProductTenorType();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new ProductTenorTypeMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new ProductTenorTypeMapping
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

        public static void CreateSearch(Search search, ProductTenorType entity1, ProductTenorType entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.ProductTenorType MakeChangeToContract(RWEST.Nexus.MDM.Contracts.ProductTenorType currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ProductTenorType contract)
        {
            var entity = ObjectMother.Create<ProductTenorType>();
            repository.Add(entity.Product);
            repository.Add(entity.TenorType);
            repository.Flush();

            contract.Details = new ProductTenorTypeDetails
            {
                Product = entity.Product.CreateNexusEntityId(() => entity.Product.Name),
                TenorType = entity.TenorType.CreateNexusEntityId(() => entity.TenorType.Name),
            };

        }

        private static void AddDetailsToEntity(ProductTenorType entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<ProductTenorType>();
            entity.Product = newEntity.Product;
            entity.TenorType = newEntity.TenorType;
        }

        private static void CreateSearchData(Search search, ProductTenorType entity1, ProductTenorType entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
