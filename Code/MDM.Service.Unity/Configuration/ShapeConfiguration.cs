namespace EnergyTrading.MDM.Configuration
{
    using Microsoft.Practices.Unity;
	using System.Collections.Generic;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;
	

    public class ShapeConfiguration : EntityConfiguration<Services.ShapeService, MDM.Shape, RWEST.Nexus.MDM.Contracts.Shape, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Shape, MDM.Shape>, EnergyTrading.MDM.Contracts.Mappers.ShapeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeDetails, MDM.Shape>, EnergyTrading.MDM.Contracts.Mappers.ShapeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ShapeMapping>, MappingMapper<ShapeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ShapeDetailsMapper());
            MappingEngine.RegisterMap(new ShapeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Shape, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Shape, RWEST.Nexus.MDM.Contracts.Shape>, MDM.Mappers.ShapeMapper>();
        }
    }
}