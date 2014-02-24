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

    public static class ProductCurveData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static ProductCurveData()
        {
            repository = ObjectScript.Repository;
        }

        public static ProductCurve CreateBasicEntity()
        {
            var entity = ObjectMother.Create<ProductCurve>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static ProductCurve CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<ProductCurve>();

            var endurMapping = new ProductCurveMapping
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

        public static RWEST.Nexus.MDM.Contracts.ProductCurve CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.ProductCurve();
            AddDetailsToContract(contract);
            return contract;
        }

        public static ProductCurve CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new ProductCurve();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new ProductCurveMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new ProductCurveMapping
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

        public static void CreateSearch(Search search, ProductCurve entity1, ProductCurve entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.ProductCurve MakeChangeToContract(RWEST.Nexus.MDM.Contracts.ProductCurve currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ProductCurve contract)
        {
			
            var entity = ObjectMother.Create<ProductCurve>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new ProductCurveDetails()
                {
                    Name = Guid.NewGuid().ToString(),
                    Curve = entity.Curve.CreateNexusEntityId(() => entity.Curve.Name),
                    Product = entity.Product.CreateNexusEntityId(() => entity.Product.Name),
                    ProductCurveType = "financial",
                    ProjectionMethod = "monthly"
                };
			
        }

        private static void AddDetailsToEntity(ProductCurve entity, DateTime startDate, DateTime endDate)
        {
			var newEntity = ObjectMother.Create<ProductCurve>();
            entity.Name = Guid.NewGuid().ToString();
            entity.Curve = newEntity.Curve;
            entity.Product = newEntity.Product;
            entity.ProductCurveType = "financial";
            entity.ProjectionMethod = "monthly";
        }

        private static void CreateSearchData(Search search, ProductCurve entity1, ProductCurve entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
