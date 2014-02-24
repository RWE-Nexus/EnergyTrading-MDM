namespace EnergyTrading.MDM.Configuration
{
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;

    public class LocationConfiguration : EntityConfiguration<Services.LocationService, MDM.Location, RWEST.Nexus.MDM.Contracts.Location, 
		LocationMapping, LocationValidator>
    {
        public LocationConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "location"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Location, MDM.Location>, EnergyTrading.MDM.Contracts.Mappers.LocationMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LocationDetails, MDM.Location>, EnergyTrading.MDM.Contracts.Mappers.LocationDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, LocationMapping>, MappingMapper<LocationMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.LocationDetailsMapper());
            MappingEngine.RegisterMap(new LocationMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Location, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Location, RWEST.Nexus.MDM.Contracts.Location>, MDM.Mappers.LocationMapper>();
        }
    }
}