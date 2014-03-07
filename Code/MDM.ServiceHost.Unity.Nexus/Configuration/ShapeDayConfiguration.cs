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

    using ShapeDay = EnergyTrading.MDM.ShapeDay;

    public class ShapeDayConfiguration : NexusEntityConfiguration<ShapeDayService, ShapeDay, RWEST.Nexus.MDM.Contracts.ShapeDay, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeDay, ShapeDay>, EnergyTrading.MDM.Contracts.Mappers.ShapeDayMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeDayDetails, ShapeDay>, EnergyTrading.MDM.Contracts.Mappers.ShapeDayDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ShapeDayMapping>, MappingMapper<ShapeDayMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ShapeDayDetailsMapper());
            this.MappingEngine.RegisterMap(new ShapeDayMappingMapper());      
            this.Container.RegisterType<IMapper<ShapeDay, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ShapeDay, RWEST.Nexus.MDM.Contracts.ShapeDay>, EnergyTrading.MDM.Mappers.ShapeDayMapper>();
        }
    }
}