namespace EnergyTrading.MDM.Configuration
{
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;

    public class CounterpartyConfiguration : EntityConfiguration<Services.CounterpartyService, MDM.Counterparty, RWEST.Nexus.MDM.Contracts.Counterparty, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Counterparty, MDM.Counterparty>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CounterpartyDetails, MDM.CounterpartyDetails>, EnergyTrading.MDM.Contracts.Mappers.CounterpartyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.CounterpartyDetailsMapper());
            MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Counterparty, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Counterparty, RWEST.Nexus.MDM.Contracts.Counterparty>, MDM.Mappers.CounterpartyMapper>();
        }
    }
}