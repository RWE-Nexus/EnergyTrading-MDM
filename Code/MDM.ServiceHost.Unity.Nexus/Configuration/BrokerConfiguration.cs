namespace MDM.ServiceHost.Unity.Nexus.Configuration
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;
    using EnergyTrading.MDM.Services;

    using Microsoft.Practices.Unity;

    using EnergyTrading.Contracts.Atom;

    using Broker = EnergyTrading.MDM.Broker;
    using BrokerDetails = EnergyTrading.MDM.BrokerDetails;

    public class BrokerConfiguration : NexusEntityConfiguration<BrokerService, Broker, OpenNexus.MDM.Contracts.Broker, 
        PartyRoleMapping, BrokerValidator>
    {
        public BrokerConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "broker"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Broker, Broker>, EnergyTrading.MDM.Contracts.Mappers.BrokerMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.BrokerDetails, BrokerDetails>, EnergyTrading.MDM.Contracts.Mappers.BrokerDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.BrokerDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<Broker, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Broker, OpenNexus.MDM.Contracts.Broker>, EnergyTrading.MDM.Mappers.BrokerMapper>();
        }
    }
}