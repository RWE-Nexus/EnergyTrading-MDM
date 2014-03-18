namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class SettlementContactService : MdmService<OpenNexus.MDM.Contracts.SettlementContact, SettlementContact, PartyAccountabilityMapping, SettlementContact, OpenNexus.MDM.Contracts.SettlementContactDetails>
	    {

    public SettlementContactService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<SettlementContact> Details(SettlementContact entity)
        {
            return new List<SettlementContact>() { entity };
	     }

        protected override IEnumerable<PartyAccountabilityMapping> Mappings(SettlementContact entity)
        {
            return entity.Mappings;
        }
    }
}