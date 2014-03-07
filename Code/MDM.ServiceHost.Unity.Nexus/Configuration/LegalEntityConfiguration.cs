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

    using LegalEntity = EnergyTrading.MDM.LegalEntity;
    using LegalEntityDetails = EnergyTrading.MDM.LegalEntityDetails;

    public class LegalEntityConfiguration : NexusEntityConfiguration<LegalEntityService, LegalEntity, RWEST.Nexus.MDM.Contracts.LegalEntity,
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LegalEntity, LegalEntity>, EnergyTrading.MDM.Contracts.Mappers.LegalEntityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.LegalEntityDetails, LegalEntityDetails>, EnergyTrading.MDM.Contracts.Mappers.LegalEntityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.LegalEntityDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());
            this.Container.RegisterType<IMapper<LegalEntity, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<LegalEntity, RWEST.Nexus.MDM.Contracts.LegalEntity>, EnergyTrading.MDM.Mappers.LegalEntityMapper>();
        }
    }
}