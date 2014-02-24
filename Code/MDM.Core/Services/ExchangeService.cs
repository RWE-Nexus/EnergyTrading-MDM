namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class ExchangeService : MdmService<RWEST.Nexus.MDM.Contracts.Exchange, Exchange, PartyRoleMapping, ExchangeDetails, RWEST.Nexus.MDM.Contracts.ExchangeDetails>
    {
        public ExchangeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) 
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<ExchangeDetails> Details(Exchange entity)
        {
            return new List<ExchangeDetails>(entity.Details.Select(x => x as ExchangeDetails));
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(Exchange entity)
        {
            return entity.Mappings;
        }
    }
}


