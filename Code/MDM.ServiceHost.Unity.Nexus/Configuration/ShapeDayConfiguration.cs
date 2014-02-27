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
	

    public class ShapeDayConfiguration : NexusEntityConfiguration<Services.ShapeDayService, MDM.ShapeDay, RWEST.Nexus.MDM.Contracts.ShapeDay, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeDay, MDM.ShapeDay>, EnergyTrading.MDM.Contracts.Mappers.ShapeDayMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShapeDayDetails, MDM.ShapeDay>, EnergyTrading.MDM.Contracts.Mappers.ShapeDayDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ShapeDayMapping>, MappingMapper<ShapeDayMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ShapeDayDetailsMapper());
            MappingEngine.RegisterMap(new ShapeDayMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ShapeDay, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ShapeDay, RWEST.Nexus.MDM.Contracts.ShapeDay>, MDM.Mappers.ShapeDayMapper>();
        }
    }
}