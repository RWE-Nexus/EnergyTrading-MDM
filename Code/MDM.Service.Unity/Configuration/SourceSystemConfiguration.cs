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
            MappingEngine.RegisterMap(new Mappers.SourceSystemDetailsMapper());
            MappingEngine.RegisterMap(new SourceSystemMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.SourceSystem, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.SourceSystem, RWEST.Nexus.MDM.Contracts.SourceSystem>, MDM.Mappers.SourceSystemMapper>();
        }
    }
}