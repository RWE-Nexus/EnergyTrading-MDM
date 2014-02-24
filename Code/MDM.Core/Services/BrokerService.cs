namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Broker = EnergyTrading.MDM.Broker;
    using BrokerDetails = EnergyTrading.MDM.BrokerDetails;

    public class BrokerService :
        MdmService<RWEST.Nexus.MDM.Contracts.Broker, Broker, PartyRoleMapping, BrokerDetails, RWEST.Nexus.MDM.Contracts.BrokerDetails>
    {
        public BrokerService(
            IValidatorEngine validatorFactory,
            IMappingEngine mappingEngine,
            IRepository repository,
            ISearchCache searchCache)
            : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<BrokerDetails> Details(Broker entity)
        {
            return new List<BrokerDetails>(entity.Details.Select(x => x as BrokerDetails));
        }

        protected override IEnumerable<PartyRoleMapping> Mappings(Broker entity)
        {
            return entity.Mappings;
        }
    }
}