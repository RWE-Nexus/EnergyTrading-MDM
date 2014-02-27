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
    using ProductScota = EnergyTrading.MDM.ProductScota;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class ProductScotaData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static ProductScotaData()
        {
            repository = ObjectScript.Repository;
        }

        public static ProductScota CreateBasicEntity()
        {
            var entity = ObjectMother.Create<ProductScota>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static ProductScota CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<ProductScota>();

            var endurMapping = new ProductScotaMapping
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

        public static RWEST.Nexus.MDM.Contracts.ProductScota CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.ProductScota();
            AddDetailsToContract(contract);
            return contract;
        }

        public static ProductScota CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new ProductScota();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new ProductScotaMapping
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new ProductScotaMapping
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

        public static void CreateSearch(Search search, ProductScota entity1, ProductScota entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.ProductScota MakeChangeToContract(RWEST.Nexus.MDM.Contracts.ProductScota currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ProductScota contract)
        {

            var entity = ObjectMother.Create<ProductScota>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new ProductScotaDetails()
                {
                    Name = entity.Name,
                    Product = entity.Product.CreateNexusEntityId(() => entity.Product.Name),
                    ScotaDeliveryPoint = entity.ScotaDeliveryPoint.CreateNexusEntityId(() => entity.ScotaDeliveryPoint.Name),
                    ScotaOrigin = entity.ScotaOrigin.CreateNexusEntityId(() => entity.ScotaOrigin.Name),
                    ScotaContract = entity.ScotaContract,
                    ScotaRss = entity.ScotaRss,
                    ScotaVersion = entity.ScotaVersion
                };
        }

        private static void AddDetailsToEntity(ProductScota entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<ProductScota>();
            entity.Name = newEntity.Name;
            entity.Product = newEntity.Product;
            entity.ScotaDeliveryPoint = newEntity.ScotaDeliveryPoint;
            entity.ScotaOrigin = newEntity.ScotaOrigin;
            entity.ScotaContract = newEntity.ScotaContract;
            entity.ScotaRss = newEntity.ScotaRss;
            entity.ScotaVersion = newEntity.ScotaVersion;
        }

        private static void CreateSearchData(Search search, ProductScota entity1, ProductScota entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
