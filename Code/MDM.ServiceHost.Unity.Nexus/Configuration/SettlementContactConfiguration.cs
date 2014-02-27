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

    public class SettlementContactConfiguration : NexusEntityConfiguration<Services.SettlementContactService, MDM.SettlementContact, RWEST.Nexus.MDM.Contracts.SettlementContact, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.SettlementContact, MDM.SettlementContact>, EnergyTrading.MDM.Contracts.Mappers.SettlementContactMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.SettlementContactDetails, MDM.SettlementContact>, EnergyTrading.MDM.Contracts.Mappers.SettlementContactDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyAccountabilityMapping>, MappingMapper<PartyAccountabilityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.SettlementContactDetailsMapper());
            MappingEngine.RegisterMap(new PartyAccountabilityMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.SettlementContact, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.SettlementContact, RWEST.Nexus.MDM.Contracts.SettlementContact>, MDM.Mappers.SettlementContactMapper>();
        }
    }
}