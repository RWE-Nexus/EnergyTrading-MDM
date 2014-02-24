namespace EnergyTrading.MDM.Configuration
{
    using Microsoft.Practices.Unity;
	using System.Collections.Generic;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;
	

    public class PartyOverrideConfiguration : EntityConfiguration<Services.PartyOverrideService, MDM.PartyOverride, RWEST.Nexus.MDM.Contracts.PartyOverride, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyOverride, MDM.PartyOverride>, EnergyTrading.MDM.Contracts.Mappers.PartyOverrideMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyOverrideDetails, MDM.PartyOverride>, EnergyTrading.MDM.Contracts.Mappers.PartyOverrideDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyOverrideMapping>, MappingMapper<PartyOverrideMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PartyOverrideDetailsMapper());
            MappingEngine.RegisterMap(new PartyOverrideMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.PartyOverride, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.PartyOverride, RWEST.Nexus.MDM.Contracts.PartyOverride>, MDM.Mappers.PartyOverrideMapper>();
        }
    }
}