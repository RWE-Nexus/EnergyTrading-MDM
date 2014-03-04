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

    using LocationRole = EnergyTrading.MDM.LocationRole;

    public class LocationRoleConfiguration : NexusEntityConfiguration<LocationRoleService, LocationRole, RWEST.Nexus.MDM.Contracts.LocationRole, 
		LocationRoleMapping, LocationRoleValidator>
    {
        public LocationRoleConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "locationrole"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LocationRole, LocationRole>, EnergyTrading.MDM.Contracts.Mappers.LocationRoleMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LocationRoleDetails, LocationRole>, EnergyTrading.MDM.Contracts.Mappers.LocationRoleDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, LocationRoleMapping>, MappingMapper<LocationRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.LocationRoleDetailsMapper());
            this.MappingEngine.RegisterMap(new LocationRoleMappingMapper());      
            this.Container.RegisterType<IMapper<LocationRole, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<LocationRole, RWEST.Nexus.MDM.Contracts.LocationRole>, EnergyTrading.MDM.Mappers.LocationRoleMapper>();
        }
    }
}