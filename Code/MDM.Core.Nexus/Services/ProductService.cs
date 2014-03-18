namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class ProductService : MdmService<OpenNexus.MDM.Contracts.Product, Product, ProductMapping, Product, OpenNexus.MDM.Contracts.ProductDetails>
    {
        public ProductService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Product> Details(Product entity)
        {
            return new List<Product> { entity };
        }

        protected override IEnumerable<ProductMapping> Mappings(Product entity)
        {
            return entity.Mappings;
        }
    }
}