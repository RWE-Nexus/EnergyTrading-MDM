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

    using BrokerCommodity = EnergyTrading.MDM.BrokerCommodity;

    public class BrokerCommodityConfiguration : NexusEntityConfiguration<BrokerCommodityService, BrokerCommodity, RWEST.Nexus.MDM.Contracts.BrokerCommodity, 
		BrokerCommodityMapping, BrokerCommodityValidator>
    {
        public BrokerCommodityConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "brokercommodity"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerCommodity, BrokerCommodity>, EnergyTrading.MDM.Contracts.Mappers.BrokerCommodityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerCommodityDetails, BrokerCommodity>, EnergyTrading.MDM.Contracts.Mappers.BrokerCommodityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, BrokerCommodityMapping>, MappingMapper<BrokerCommodityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.BrokerCommodityDetailsMapper());
            this.MappingEngine.RegisterMap(new BrokerCommodityMappingMapper());      
            this.Container.RegisterType<IMapper<BrokerCommodity, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<BrokerCommodity, RWEST.Nexus.MDM.Contracts.BrokerCommodity>, EnergyTrading.MDM.Mappers.BrokerCommodityMapper>();
        }
    }
}