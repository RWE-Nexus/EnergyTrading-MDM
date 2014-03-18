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

    using EnergyTrading.Contracts.Atom;

    using Party = EnergyTrading.MDM.Party;
    using PartyDetails = EnergyTrading.MDM.PartyDetails;

    public class PartyConfiguration : NexusEntityConfiguration<PartyService, Party, OpenNexus.MDM.Contracts.Party, PartyMapping, PartyValidator>
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Party, Party>, EnergyTrading.MDM.Contracts.Mappers.PartyMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.PartyDetails, PartyDetails>, EnergyTrading.MDM.Contracts.Mappers.PartyDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyMapping>, MappingMapper<PartyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PartyDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyMappingMapper());
            this.Container.RegisterType<IMapper<Party, List<Link>>, PartyLinksMapper>();
            this.Container.RegisterType<IMapper<Party, OpenNexus.MDM.Contracts.Party>, EnergyTrading.MDM.Mappers.PartyMapper>();
        }
    }
}