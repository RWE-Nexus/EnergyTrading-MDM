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

    using SettlementContact = EnergyTrading.MDM.SettlementContact;

    public class SettlementContactConfiguration : NexusEntityConfiguration<SettlementContactService, SettlementContact, RWEST.Nexus.MDM.Contracts.SettlementContact, 
		PartyAccountabilityMapping, SettlementContactValidator>
    {
        public SettlementContactConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "settlementcontact"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.SettlementContact, SettlementContact>, EnergyTrading.MDM.Contracts.Mappers.SettlementContactMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.SettlementContactDetails, SettlementContact>, EnergyTrading.MDM.Contracts.Mappers.SettlementContactDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyAccountabilityMapping>, MappingMapper<PartyAccountabilityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.SettlementContactDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyAccountabilityMappingMapper());      
            this.Container.RegisterType<IMapper<SettlementContact, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<SettlementContact, RWEST.Nexus.MDM.Contracts.SettlementContact>, EnergyTrading.MDM.Mappers.SettlementContactMapper>();
        }
    }
}