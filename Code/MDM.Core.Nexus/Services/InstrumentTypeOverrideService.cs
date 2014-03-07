namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class InstrumentTypeOverrideService : MdmService<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride, InstrumentTypeOverride, InstrumentTypeOverrideMapping, InstrumentTypeOverride, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails>
	    {

    public InstrumentTypeOverrideService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<InstrumentTypeOverride> Details(InstrumentTypeOverride entity)
        {
			return new List<InstrumentTypeOverride> { entity };
	        }

        protected override IEnumerable<InstrumentTypeOverrideMapping> Mappings(InstrumentTypeOverride entity)
        {
            return entity.Mappings;
        }
    }
}