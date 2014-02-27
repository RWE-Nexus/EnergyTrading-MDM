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

    public class PartyRoleConfiguration : NexusEntityConfiguration<Services.PartyRoleService, MDM.PartyRole, RWEST.Nexus.MDM.Contracts.PartyRole, PartyRoleMapping, PartyRoleValidator>
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRole, MDM.PartyRole>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRoleDetails, MDM.PartyRoleDetails>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PartyRoleDetailsMapper());
            MappingEngine.RegisterMap(new PartyRoleMappingMapper());
            this.Container.RegisterType<IMapper<MDM.PartyRole, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.PartyRole, RWEST.Nexus.MDM.Contracts.PartyRole>, MDM.Mappers.PartyRoleMapper>();
        }
    }
}
