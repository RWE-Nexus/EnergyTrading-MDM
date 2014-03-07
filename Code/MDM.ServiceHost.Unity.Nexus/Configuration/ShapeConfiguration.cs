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

    using Shape = EnergyTrading.MDM.Shape;

    public class ShapeConfiguration : NexusEntityConfiguration<ShapeService, Shape, RWEST.Nexus.MDM.Contracts.Shape, 
		ShapeMapping, ShapeValidator>
    {
        public ShapeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "shape"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Shape, Shape>, EnergyTrading.MDM.Contracts.Mappers.ShapeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeDetails, Shape>, EnergyTrading.MDM.Contracts.Mappers.ShapeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ShapeMapping>, MappingMapper<ShapeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ShapeDetailsMapper());
            this.MappingEngine.RegisterMap(new ShapeMappingMapper());      
            this.Container.RegisterType<IMapper<Shape, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Shape, RWEST.Nexus.MDM.Contracts.Shape>, EnergyTrading.MDM.Mappers.ShapeMapper>();
        }
    }
}