namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class PartyAccountabilityService : MdmService<OpenNexus.MDM.Contracts.PartyAccountability, PartyAccountability, PartyAccountabilityMapping, PartyAccountability, OpenNexus.MDM.Contracts.PartyAccountabilityDetails>
	    {

    public PartyAccountabilityService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<PartyAccountability> Details(PartyAccountability entity)
        {
            return new List<PartyAccountability> { entity };
        }

        protected override IEnumerable<PartyAccountabilityMapping> Mappings(PartyAccountability entity)
        {
            return entity.Mappings;
        }
    }
}