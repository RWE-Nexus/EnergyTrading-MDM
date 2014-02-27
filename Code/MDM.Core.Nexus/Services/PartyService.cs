namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class PartyService : MdmService<RWEST.Nexus.MDM.Contracts.Party, Party, PartyMapping, PartyDetails, RWEST.Nexus.MDM.Contracts.PartyDetails>
    {
        public PartyService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) 
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<PartyDetails> Details(Party entity)
        {
            return entity.Details;
        }

        protected override IEnumerable<PartyMapping> Mappings(Party entity)
        {
            return entity.Mappings;
        }
    }
}
