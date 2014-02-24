namespace EnergyTrading.MDM.Configuration
{
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;
    

    public class LegalEntityConfiguration : EntityConfiguration<Services.LegalEntityService, MDM.LegalEntity, RWEST.Nexus.MDM.Contracts.LegalEntity,
        PartyRoleMapping, LegalEntityValidator>
    {
        public LegalEntityConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "legalentity"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LegalEntity, MDM.LegalEntity>, EnergyTrading.MDM.Contracts.Mappers.LegalEntityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LegalEntityDetails, MDM.LegalEntityDetails>, EnergyTrading.MDM.Contracts.Mappers.LegalEntityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.LegalEntityDetailsMapper());
            MappingEngine.RegisterMap(new PartyRoleMappingMapper());
            this.Container.RegisterType<IMapper<MDM.LegalEntity, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.LegalEntity, RWEST.Nexus.MDM.Contracts.LegalEntity>, MDM.Mappers.LegalEntityMapper>();
        }
    }
}