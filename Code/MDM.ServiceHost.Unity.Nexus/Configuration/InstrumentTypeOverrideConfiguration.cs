

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

    using InstrumentTypeOverride = EnergyTrading.MDM.InstrumentTypeOverride;

    public class InstrumentTypeOverrideConfiguration : NexusEntityConfiguration<InstrumentTypeOverrideService, InstrumentTypeOverride, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride, InstrumentTypeOverride>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeOverrideMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails, InstrumentTypeOverride>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeOverrideDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, InstrumentTypeOverrideMapping>, MappingMapper<InstrumentTypeOverrideMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.InstrumentTypeOverrideDetailsMapper());
            this.MappingEngine.RegisterMap(new InstrumentTypeOverrideMappingMapper());      
            this.Container.RegisterType<IMapper<InstrumentTypeOverride, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<InstrumentTypeOverride, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride>, EnergyTrading.MDM.Mappers.InstrumentTypeOverrideMapper>();
        }
    }
}