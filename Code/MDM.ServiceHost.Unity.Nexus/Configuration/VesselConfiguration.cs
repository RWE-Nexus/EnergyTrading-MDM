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
	

    public class VesselConfiguration : NexusEntityConfiguration<Services.VesselService, MDM.Vessel, RWEST.Nexus.MDM.Contracts.Vessel, 
		VesselMapping, VesselValidator>
    {
        public VesselConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "vessel"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Vessel, MDM.Vessel>, EnergyTrading.MDM.Contracts.Mappers.VesselMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.VesselDetails, MDM.Vessel>, EnergyTrading.MDM.Contracts.Mappers.VesselDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, VesselMapping>, MappingMapper<VesselMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.VesselDetailsMapper());
            MappingEngine.RegisterMap(new VesselMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Vessel, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Vessel, RWEST.Nexus.MDM.Contracts.Vessel>, MDM.Mappers.VesselMapper>();
        }
    }
}