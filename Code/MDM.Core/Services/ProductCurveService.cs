namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class ProductCurveService : MdmService<RWEST.Nexus.MDM.Contracts.ProductCurve, ProductCurve, ProductCurveMapping, ProductCurve, RWEST.Nexus.MDM.Contracts.ProductCurveDetails>
	    {

    public ProductCurveService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<ProductCurve> Details(ProductCurve entity)
        {
			return new List<ProductCurve> { entity };
	        }

        protected override IEnumerable<ProductCurveMapping> Mappings(ProductCurve entity)
        {
            return entity.Mappings;
        }
    }
}