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

    using Vessel = EnergyTrading.MDM.Vessel;

    public class VesselConfiguration : NexusEntityConfiguration<VesselService, Vessel, OpenNexus.MDM.Contracts.Vessel, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Vessel, Vessel>, EnergyTrading.MDM.Contracts.Mappers.VesselMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.VesselDetails, Vessel>, EnergyTrading.MDM.Contracts.Mappers.VesselDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, VesselMapping>, MappingMapper<VesselMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.VesselDetailsMapper());
            this.MappingEngine.RegisterMap(new VesselMappingMapper());      
            this.Container.RegisterType<IMapper<Vessel, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Vessel, OpenNexus.MDM.Contracts.Vessel>, EnergyTrading.MDM.Mappers.VesselMapper>();
        }
    }
}