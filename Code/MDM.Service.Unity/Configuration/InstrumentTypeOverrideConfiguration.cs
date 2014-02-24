

namespace EnergyTrading.MDM.Configuration
{
    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;
    using System.Collections.Generic;

    public class InstrumentTypeOverrideConfiguration : EntityConfiguration<Services.InstrumentTypeOverrideService, MDM.InstrumentTypeOverride, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride, 
		InstrumentTypeOverrideMapping, InstrumentTypeOverrideValidator>
    {
        public InstrumentTypeOverrideConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "instrumenttypeoverride"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride, MDM.InstrumentTypeOverride>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeOverrideMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails, MDM.InstrumentTypeOverride>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeOverrideDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, InstrumentTypeOverrideMapping>, MappingMapper<InstrumentTypeOverrideMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.InstrumentTypeOverrideDetailsMapper());
            MappingEngine.RegisterMap(new InstrumentTypeOverrideMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.InstrumentTypeOverride, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.InstrumentTypeOverride, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride>, MDM.Mappers.InstrumentTypeOverrideMapper>();
        }
    }
}