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

    using Counterparty = EnergyTrading.MDM.Counterparty;
    using CounterpartyDetails = EnergyTrading.MDM.CounterpartyDetails;

    public class CounterpartyConfiguration : NexusEntityConfiguration<CounterpartyService, Counterparty, OpenNexus.MDM.Contracts.Counterparty, 
		PartyRoleMapping, CounterpartyValidator>
    {
        public CounterpartyConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "counterparty"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Counterparty, Counterparty>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.CounterpartyDetails, CounterpartyDetails>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CounterpartyDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<Counterparty, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Counterparty, OpenNexus.MDM.Contracts.Counterparty>, EnergyTrading.MDM.Mappers.CounterpartyMapper>();
        }
    }
}