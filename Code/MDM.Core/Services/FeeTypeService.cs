namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class FeeTypeService : MdmService<RWEST.Nexus.MDM.Contracts.FeeType, FeeType, FeeTypeMapping, FeeType, RWEST.Nexus.MDM.Contracts.FeeTypeDetails>
	    {

    public FeeTypeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<FeeType> Details(FeeType entity)
        {
			return new List<FeeType> { entity };
	        }

        protected override IEnumerable<FeeTypeMapping> Mappings(FeeType entity)
        {
            return entity.Mappings;
        }
    }
}