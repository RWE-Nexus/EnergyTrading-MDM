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

    public class LocationRoleConfiguration : NexusEntityConfiguration<Services.LocationRoleService, MDM.LocationRole, RWEST.Nexus.MDM.Contracts.LocationRole, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LocationRole, MDM.LocationRole>, EnergyTrading.MDM.Contracts.Mappers.LocationRoleMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LocationRoleDetails, MDM.LocationRole>, EnergyTrading.MDM.Contracts.Mappers.LocationRoleDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, LocationRoleMapping>, MappingMapper<LocationRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.LocationRoleDetailsMapper());
            MappingEngine.RegisterMap(new LocationRoleMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.LocationRole, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.LocationRole, RWEST.Nexus.MDM.Contracts.LocationRole>, MDM.Mappers.LocationRoleMapper>();
        }
    }
}