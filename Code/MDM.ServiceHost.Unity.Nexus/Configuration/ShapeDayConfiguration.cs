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

    using ShapeDay = EnergyTrading.MDM.ShapeDay;

    public class ShapeDayConfiguration : NexusEntityConfiguration<ShapeDayService, ShapeDay, OpenNexus.MDM.Contracts.ShapeDay, 
		ShapeDayMapping, ShapeDayValidator>
    {
        public ShapeDayConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "shapeday"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ShapeDay, ShapeDay>, EnergyTrading.MDM.Contracts.Mappers.ShapeDayMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ShapeDayDetails, ShapeDay>, EnergyTrading.MDM.Contracts.Mappers.ShapeDayDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, ShapeDayMapping>, MappingMapper<ShapeDayMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ShapeDayDetailsMapper());
            this.MappingEngine.RegisterMap(new ShapeDayMappingMapper());      
            this.Container.RegisterType<IMapper<ShapeDay, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ShapeDay, OpenNexus.MDM.Contracts.ShapeDay>, EnergyTrading.MDM.Mappers.ShapeDayMapper>();
        }
    }
}