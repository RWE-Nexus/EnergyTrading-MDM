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

    using RWEST.Nexus.Contracts.Atom;

    using Counterparty = EnergyTrading.MDM.Counterparty;
    using CounterpartyDetails = EnergyTrading.MDM.CounterpartyDetails;

    public class CounterpartyConfiguration : NexusEntityConfiguration<CounterpartyService, Counterparty, RWEST.Nexus.MDM.Contracts.Counterparty, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Counterparty, Counterparty>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CounterpartyDetails, CounterpartyDetails>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CounterpartyDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<Counterparty, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Counterparty, RWEST.Nexus.MDM.Contracts.Counterparty>, EnergyTrading.MDM.Mappers.CounterpartyMapper>();
        }
    }
}