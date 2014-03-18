namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using Broker = EnergyTrading.MDM.Broker;
    using BrokerDetails = EnergyTrading.MDM.BrokerDetails;

    public class BrokerService :
        MdmService<OpenNexus.MDM.Contracts.Broker, Broker, PartyRoleMapping, BrokerDetails, OpenNexus.MDM.Contracts.BrokerDetails>
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