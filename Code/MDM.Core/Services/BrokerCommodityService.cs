namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class BrokerCommodityService : MdmService<RWEST.Nexus.MDM.Contracts.BrokerCommodity, BrokerCommodity, BrokerCommodityMapping, BrokerCommodity, RWEST.Nexus.MDM.Contracts.BrokerCommodityDetails>
	    {

    public BrokerCommodityService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<BrokerCommodity> Details(BrokerCommodity entity)
        {
			return new List<BrokerCommodity> { entity };
	        }

        protected override IEnumerable<BrokerCommodityMapping> Mappings(BrokerCommodity entity)
        {
            return entity.Mappings;
        }
    }
}