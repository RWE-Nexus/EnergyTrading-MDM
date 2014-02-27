namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class BrokerRateService : MdmService<RWEST.Nexus.MDM.Contracts.BrokerRate, BrokerRate, BrokerRateMapping, BrokerRateDetails, RWEST.Nexus.MDM.Contracts.BrokerRateDetails>
	    {

    public BrokerRateService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<BrokerRateDetails> Details(BrokerRate entity)
        {
            return entity.Details;
        }

        protected override IEnumerable<BrokerRateMapping> Mappings(BrokerRate entity)
        {
            return entity.Mappings;
        }
    }
}