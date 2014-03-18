namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class ProductScotaService : MdmService<OpenNexus.MDM.Contracts.ProductScota, ProductScota, ProductScotaMapping, ProductScota, OpenNexus.MDM.Contracts.ProductScotaDetails>
	    {

    public ProductScotaService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<ProductScota> Details(ProductScota entity)
        {
			return new List<ProductScota> { entity };
	        }

        protected override IEnumerable<ProductScotaMapping> Mappings(ProductScota entity)
        {
            return entity.Mappings;
        }
    }
}