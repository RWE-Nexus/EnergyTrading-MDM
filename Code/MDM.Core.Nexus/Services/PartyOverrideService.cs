namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class PartyOverrideService : MdmService<OpenNexus.MDM.Contracts.PartyOverride, PartyOverride, PartyOverrideMapping, PartyOverride, OpenNexus.MDM.Contracts.PartyOverrideDetails>
	    {

    public PartyOverrideService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<PartyOverride> Details(PartyOverride entity)
        {
			return new List<PartyOverride> { entity };
	        }

        protected override IEnumerable<PartyOverrideMapping> Mappings(PartyOverride entity)
        {
            return entity.Mappings;
        }
    }
}