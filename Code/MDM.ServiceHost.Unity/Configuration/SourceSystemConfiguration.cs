namespace EnergyTrading.MDM.ServiceHost.Unity.Configuration
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;

    using Microsoft.Practices.Unity;

    public class SourceSystemConfiguration : EntityConfiguration<Services.SourceSystemService, MDM.SourceSystem, EnergyTrading.Mdm.Contracts.SourceSystem, SourceSystemMapping, SourceSystemValidator>
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
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.SourceSystem, MDM.SourceSystem>, EnergyTrading.MDM.Contracts.Mappers.SourceSystemMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.SourceSystemDetails, MDM.SourceSystem>, EnergyTrading.MDM.Contracts.Mappers.SourceSystemDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, SourceSystemMapping>, MappingMapper<SourceSystemMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new Mappers.SourceSystemDetailsMapper());
            this.MappingEngine.RegisterMap(new SourceSystemMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.SourceSystem, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.SourceSystem, EnergyTrading.Mdm.Contracts.SourceSystem>, MDM.Mappers.SourceSystemMapper>();
        }
    }
}