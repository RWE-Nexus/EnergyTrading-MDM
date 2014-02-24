namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class MarketService : MdmService<RWEST.Nexus.MDM.Contracts.Market, Market, MarketMapping, Market, RWEST.Nexus.MDM.Contracts.MarketDetails>
    {
        public MarketService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Market> Details(Market entity)
        {
            return new List<Market> { entity };
        }

        protected override IEnumerable<MarketMapping> Mappings(Market entity)
        {
            return entity.Mappings;
        }
    }
}