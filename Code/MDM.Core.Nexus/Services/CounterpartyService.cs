namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class CounterpartyService : MdmService<RWEST.Nexus.MDM.Contracts.Counterparty, Counterparty, PartyRoleMapping, CounterpartyDetails, RWEST.Nexus.MDM.Contracts.CounterpartyDetails>
    {

        public CounterpartyService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<CounterpartyDetails> Details(Counterparty entity)
        {
            return new List<CounterpartyDetails>(entity.Details.Select(x => x as CounterpartyDetails));
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(Counterparty entity)
        {
            return entity.Mappings;
        }
    }
}