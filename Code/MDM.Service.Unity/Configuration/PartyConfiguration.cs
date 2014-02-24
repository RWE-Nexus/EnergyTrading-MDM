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

    public class PartyConfiguration : EntityConfiguration<Services.PartyService, MDM.Party, RWEST.Nexus.MDM.Contracts.Party, PartyMapping, PartyValidator>
    {
        public PartyConfiguration(IUnityContainer container)
            : base(container)
        {
        }

        protected override string Name
        {
            get { return "party"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Party, MDM.Party>, EnergyTrading.MDM.Contracts.Mappers.PartyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyDetails, MDM.PartyDetails>, EnergyTrading.MDM.Contracts.Mappers.PartyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyMapping>, MappingMapper<PartyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PartyDetailsMapper());
            MappingEngine.RegisterMap(new PartyMappingMapper());
            this.Container.RegisterType<IMapper<MDM.Party, List<Link>>, PartyLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Party, RWEST.Nexus.MDM.Contracts.Party>, MDM.Mappers.PartyMapper>();
        }
    }
}