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

    using Party = EnergyTrading.MDM.Party;
    using PartyDetails = EnergyTrading.MDM.PartyDetails;

    public class PartyConfiguration : NexusEntityConfiguration<PartyService, Party, RWEST.Nexus.MDM.Contracts.Party, PartyMapping, PartyValidator>
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Party, Party>, EnergyTrading.MDM.Contracts.Mappers.PartyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PartyDetails, PartyDetails>, EnergyTrading.MDM.Contracts.Mappers.PartyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyMapping>, MappingMapper<PartyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyMappingMapper());
            this.Container.RegisterType<IMapper<Party, List<Link>>, PartyLinksMapper>();
            this.Container.RegisterType<IMapper<Party, RWEST.Nexus.MDM.Contracts.Party>, EnergyTrading.MDM.Mappers.PartyMapper>();
        }
    }
}