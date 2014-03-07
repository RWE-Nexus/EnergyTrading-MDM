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

    using Market = EnergyTrading.MDM.Market;

    public class MarketConfiguration : NexusEntityConfiguration<MarketService, Market, RWEST.Nexus.MDM.Contracts.Market, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Market, Market>, EnergyTrading.MDM.Contracts.Mappers.MarketMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.MarketDetails, Market>, EnergyTrading.MDM.Contracts.Mappers.MarketDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, MarketMapping>, MappingMapper<MarketMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.MarketDetailsMapper());
            this.MappingEngine.RegisterMap(new MarketMappingMapper());      
            this.Container.RegisterType<IMapper<Market, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Market, RWEST.Nexus.MDM.Contracts.Market>, EnergyTrading.MDM.Mappers.MarketMapper>();
        }
    }
}