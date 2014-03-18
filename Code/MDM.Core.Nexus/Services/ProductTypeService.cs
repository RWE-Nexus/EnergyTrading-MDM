namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class ProductTypeService : MdmService<OpenNexus.MDM.Contracts.ProductType, ProductType, ProductTypeMapping, ProductType, OpenNexus.MDM.Contracts.ProductTypeDetails>
    {
        public ProductTypeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<ProductType> Details(ProductType entity)
        {
            return new List<ProductType> { entity };
        }

        protected override IEnumerable<ProductTypeMapping> Mappings(ProductType entity)
        {
            return entity.Mappings;
        }
    }
}