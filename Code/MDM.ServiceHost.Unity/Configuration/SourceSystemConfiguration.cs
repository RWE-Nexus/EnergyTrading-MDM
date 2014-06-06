namespace EnergyTrading.Mdm.ServiceHost.Unity.Configuration
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts.Mappers;
    using EnergyTrading.Mdm.Contracts.Validators;
    using EnergyTrading.Mdm.Mappers;

    using Microsoft.Practices.Unity;

    public class SourceSystemConfiguration : EntityConfiguration<Services.SourceSystemService, Mdm.SourceSystem, EnergyTrading.Mdm.Contracts.SourceSystem, SourceSystemMapping, SourceSystemValidator>
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
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.SourceSystem, Mdm.SourceSystem>, EnergyTrading.Mdm.Contracts.Mappers.SourceSystemMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.SourceSystemDetails, Mdm.SourceSystem>, EnergyTrading.Mdm.Contracts.Mappers.SourceSystemDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, SourceSystemMapping>, MappingMapper<SourceSystemMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new Mappers.SourceSystemDetailsMapper());
            this.MappingEngine.RegisterMap(new SourceSystemMappingMapper());      
            this.Container.RegisterType<IMapper<Mdm.SourceSystem, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Mdm.SourceSystem, EnergyTrading.Mdm.Contracts.SourceSystem>, Mdm.Mappers.SourceSystemMapper>();
        }
    }
}