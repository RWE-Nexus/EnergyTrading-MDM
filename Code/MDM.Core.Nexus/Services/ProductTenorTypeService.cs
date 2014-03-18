namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class ProductTenorTypeService : MdmService<OpenNexus.MDM.Contracts.ProductTenorType, ProductTenorType, ProductTenorTypeMapping, ProductTenorType, OpenNexus.MDM.Contracts.ProductTenorTypeDetails>
	    {

    public ProductTenorTypeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<ProductTenorType> Details(ProductTenorType entity)
        {
			return new List<ProductTenorType> { entity };
	        }

        protected override IEnumerable<ProductTenorTypeMapping> Mappings(ProductTenorType entity)
        {
            return entity.Mappings;
        }
    }
}