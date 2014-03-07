﻿namespace MDM.ServiceHost.Unity.Nexus.Configuration
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

    using PartyRoleAccountability = EnergyTrading.MDM.PartyRoleAccountability;

    public class PartyRoleAccountabilityConfiguration : NexusEntityConfiguration<PartyRoleAccountabilityService, PartyRoleAccountability, RWEST.Nexus.MDM.Contracts.PartyRoleAccountability,
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRoleAccountability, PartyRoleAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleAccountabilityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyRoleAccountabilityDetails, PartyRoleAccountability>, EnergyTrading.MDM.Contracts.Mappers.PartyRoleAccountabilityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleAccountabilityMapping>, MappingMapper<PartyRoleAccountabilityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyRoleAccountabilityDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleAccountabilityMappingMapper());
            this.Container.RegisterType<IMapper<PartyRoleAccountability, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<PartyRoleAccountability, RWEST.Nexus.MDM.Contracts.PartyRoleAccountability>, EnergyTrading.MDM.Mappers.PartyRoleAccountabilityMapper>();
        }
    }
}