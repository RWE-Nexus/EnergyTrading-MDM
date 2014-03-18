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

    using EnergyTrading.Contracts.Atom;

    using ShapeElement = EnergyTrading.MDM.ShapeElement;

    public class ShapeElementConfiguration : NexusEntityConfiguration<ShapeElementService, ShapeElement, OpenNexus.MDM.Contracts.ShapeElement, 
		ShapeElementMapping, ShapeElementValidator>
    {
        public ShapeElementConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "shapeelement"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ShapeElement, ShapeElement>, EnergyTrading.MDM.Contracts.Mappers.ShapeElementMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ShapeElementDetails, ShapeElement>, EnergyTrading.MDM.Contracts.Mappers.ShapeElementDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, ShapeElementMapping>, MappingMapper<ShapeElementMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ShapeElementDetailsMapper());
            this.MappingEngine.RegisterMap(new ShapeElementMappingMapper());      
            this.Container.RegisterType<IMapper<ShapeElement, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ShapeElement, OpenNexus.MDM.Contracts.ShapeElement>, EnergyTrading.MDM.Mappers.ShapeElementMapper>();
        }
    }
}