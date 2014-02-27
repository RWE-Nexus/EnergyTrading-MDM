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
	

    public class ShapeElementConfiguration : NexusEntityConfiguration<Services.ShapeElementService, MDM.ShapeElement, RWEST.Nexus.MDM.Contracts.ShapeElement, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeElement, MDM.ShapeElement>, EnergyTrading.MDM.Contracts.Mappers.ShapeElementMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeElementDetails, MDM.ShapeElement>, EnergyTrading.MDM.Contracts.Mappers.ShapeElementDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ShapeElementMapping>, MappingMapper<ShapeElementMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ShapeElementDetailsMapper());
            MappingEngine.RegisterMap(new ShapeElementMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ShapeElement, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ShapeElement, RWEST.Nexus.MDM.Contracts.ShapeElement>, MDM.Mappers.ShapeElementMapper>();
        }
    }
}