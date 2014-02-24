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

    public class PartyRoleAccountabilityConfiguration : EntityConfiguration<Services.PartyRoleAccountabilityService, MDM.PartyRoleAccountability, RWEST.Nexus.MDM.Contracts.PartyRoleAccountability,
        PartyRoleAccountabilityMapping, PartyRoleAccountabilityValidator>
    {
        public PartyRoleAccountabilityConfiguration(IUnityContainer container)
            : base(container)
        {
        }

        protected override string Name
        {
            get { return "partyroleaccountability"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRoleAccountability, MDM.PartyRoleAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleAccountabilityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRoleAccountabilityDetails, MDM.PartyRoleAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleAccountabilityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleAccountabilityMapping>, MappingMapper<PartyRoleAccountabilityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PartyRoleAccountabilityDetailsMapper());
            MappingEngine.RegisterMap(new PartyRoleAccountabilityMappingMapper());
            this.Container.RegisterType<IMapper<MDM.PartyRoleAccountability, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.PartyRoleAccountability, RWEST.Nexus.MDM.Contracts.PartyRoleAccountability>, MDM.Mappers.PartyRoleAccountabilityMapper>();
        }
    }
}