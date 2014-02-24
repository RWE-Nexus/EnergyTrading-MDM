namespace EnergyTrading.MDM.Configuration
{
    using Microsoft.Practices.Unity;
	using System.Collections.Generic;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;
	

    public class BrokerCommodityConfiguration : EntityConfiguration<Services.BrokerCommodityService, MDM.BrokerCommodity, RWEST.Nexus.MDM.Contracts.BrokerCommodity, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerCommodity, MDM.BrokerCommodity>, EnergyTrading.MDM.Contracts.Mappers.BrokerCommodityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerCommodityDetails, MDM.BrokerCommodity>, EnergyTrading.MDM.Contracts.Mappers.BrokerCommodityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, BrokerCommodityMapping>, MappingMapper<BrokerCommodityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.BrokerCommodityDetailsMapper());
            MappingEngine.RegisterMap(new BrokerCommodityMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.BrokerCommodity, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.BrokerCommodity, RWEST.Nexus.MDM.Contracts.BrokerCommodity>, MDM.Mappers.BrokerCommodityMapper>();
        }
    }
}