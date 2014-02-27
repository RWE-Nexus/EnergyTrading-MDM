namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class CommodityFeeTypeService : MdmService<RWEST.Nexus.MDM.Contracts.CommodityFeeType, CommodityFeeType, CommodityFeeTypeMapping, CommodityFeeType, RWEST.Nexus.MDM.Contracts.CommodityFeeTypeDetails>
	    {

    public CommodityFeeTypeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<CommodityFeeType> Details(CommodityFeeType entity)
        {
			return new List<CommodityFeeType> { entity };
	        }

        protected override IEnumerable<CommodityFeeTypeMapping> Mappings(CommodityFeeType entity)
        {
            return entity.Mappings;
        }
    }
}