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

    using PartyRole = EnergyTrading.MDM.PartyRole;
    using PartyRoleDetails = EnergyTrading.MDM.PartyRoleDetails;

    public class PartyRoleConfiguration : NexusEntityConfiguration<PartyRoleService, PartyRole, RWEST.Nexus.MDM.Contracts.PartyRole, PartyRoleMapping, PartyRoleValidator>
    {
        public PartyRoleConfiguration(IUnityContainer container)
            : base(container)
        {
        }

        protected override string Name
        {
            get { return "partyrole"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRole, PartyRole>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRoleDetails, PartyRoleDetails>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyRoleDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());
            this.Container.RegisterType<IMapper<PartyRole, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<PartyRole, RWEST.Nexus.MDM.Contracts.PartyRole>, EnergyTrading.MDM.Mappers.PartyRoleMapper>();
        }
    }
}
