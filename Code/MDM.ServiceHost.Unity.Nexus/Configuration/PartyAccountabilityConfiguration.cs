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

    public class PartyAccountabilityConfiguration : NexusEntityConfiguration<Services.PartyAccountabilityService, MDM.PartyAccountability, RWEST.Nexus.MDM.Contracts.PartyAccountability, 
		PartyAccountabilityMapping, PartyAccountabilityValidator>
    {
        public PartyAccountabilityConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "partyaccountability"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyAccountability, MDM.PartyAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyAccountabilityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyAccountabilityDetails, MDM.PartyAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyAccountabilityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyAccountabilityMapping>, MappingMapper<PartyAccountabilityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PartyAccountabilityDetailsMapper());
            MappingEngine.RegisterMap(new PartyAccountabilityMappingMapper());
            this.Container.RegisterType<IMapper<MDM.PartyAccountability, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.PartyAccountability, RWEST.Nexus.MDM.Contracts.PartyAccountability>, MDM.Mappers.PartyAccountabilityMapper>();
        }
    }
}