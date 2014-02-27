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

    public class MarketConfiguration : NexusEntityConfiguration<Services.MarketService, MDM.Market, RWEST.Nexus.MDM.Contracts.Market, 
		MarketMapping, MarketValidator>
    {
        public MarketConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "market"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Market, MDM.Market>, EnergyTrading.MDM.Contracts.Mappers.MarketMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.MarketDetails, MDM.Market>, EnergyTrading.MDM.Contracts.Mappers.MarketDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, MarketMapping>, MappingMapper<MarketMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.MarketDetailsMapper());
            MappingEngine.RegisterMap(new MarketMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Market, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Market, RWEST.Nexus.MDM.Contracts.Market>, MDM.Mappers.MarketMapper>();
        }
    }
}