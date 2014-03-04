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

    using PartyOverride = EnergyTrading.MDM.PartyOverride;

    public class PartyOverrideConfiguration : NexusEntityConfiguration<PartyOverrideService, PartyOverride, RWEST.Nexus.MDM.Contracts.PartyOverride, 
		PartyOverrideMapping, PartyOverrideValidator>
    {
        public PartyOverrideConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "partyoverride"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyOverride, PartyOverride>, EnergyTrading.MDM.Contracts.Mappers.PartyOverrideMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyOverrideDetails, PartyOverride>, EnergyTrading.MDM.Contracts.Mappers.PartyOverrideDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyOverrideMapping>, MappingMapper<PartyOverrideMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyOverrideDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyOverrideMappingMapper());      
            this.Container.RegisterType<IMapper<PartyOverride, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<PartyOverride, RWEST.Nexus.MDM.Contracts.PartyOverride>, EnergyTrading.MDM.Mappers.PartyOverrideMapper>();
        }
    }
}