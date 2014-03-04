namespace EnergyTrading.MDM.ServiceHost.Unity.Configuration
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;

    public class SourceSystemConfiguration : EntityConfiguration<Services.SourceSystemService, MDM.SourceSystem, RWEST.Nexus.MDM.Contracts.SourceSystem, 
		SourceSystemMapping, SourceSystemValidator>
    {
        public SourceSystemConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "sourcesystem"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.SourceSystem, MDM.SourceSystem>, EnergyTrading.MDM.Contracts.Mappers.SourceSystemMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.SourceSystemDetails, MDM.SourceSystem>, EnergyTrading.MDM.Contracts.Mappers.SourceSystemDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, SourceSystemMapping>, MappingMapper<SourceSystemMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new Mappers.SourceSystemDetailsMapper());
            this.MappingEngine.RegisterMap(new SourceSystemMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.SourceSystem, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.SourceSystem, RWEST.Nexus.MDM.Contracts.SourceSystem>, MDM.Mappers.SourceSystemMapper>();
        }
    }
}