namespace EnergyTrading.MDM.Configuration
{
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;

    public class BrokerConfiguration : EntityConfiguration<Services.BrokerService, MDM.Broker, RWEST.Nexus.MDM.Contracts.Broker, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Broker, MDM.Broker>, EnergyTrading.MDM.Contracts.Mappers.BrokerMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerDetails, MDM.BrokerDetails>, EnergyTrading.MDM.Contracts.Mappers.BrokerDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.BrokerDetailsMapper());
            MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Broker, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Broker, RWEST.Nexus.MDM.Contracts.Broker>, MDM.Mappers.BrokerMapper>();
        }
    }
}