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

    using PartyAccountability = EnergyTrading.MDM.PartyAccountability;

    public class PartyAccountabilityConfiguration : NexusEntityConfiguration<PartyAccountabilityService, PartyAccountability, RWEST.Nexus.MDM.Contracts.PartyAccountability, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyAccountability, PartyAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyAccountabilityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyAccountabilityDetails, PartyAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyAccountabilityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyAccountabilityMapping>, MappingMapper<PartyAccountabilityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyAccountabilityDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyAccountabilityMappingMapper());
            this.Container.RegisterType<IMapper<PartyAccountability, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<PartyAccountability, RWEST.Nexus.MDM.Contracts.PartyAccountability>, EnergyTrading.MDM.Mappers.PartyAccountabilityMapper>();
        }
    }
}