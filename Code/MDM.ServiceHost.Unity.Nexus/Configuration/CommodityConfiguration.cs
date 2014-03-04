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

    using Commodity = EnergyTrading.MDM.Commodity;

    public class CommodityConfiguration : NexusEntityConfiguration<CommodityService, Commodity, RWEST.Nexus.MDM.Contracts.Commodity, 
		CommodityMapping, CommodityValidator>
    {
        public CommodityConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "commodity"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Commodity, Commodity>, EnergyTrading.MDM.Contracts.Mappers.CommodityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityDetails, Commodity>, EnergyTrading.MDM.Contracts.Mappers.CommodityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CommodityMapping>, MappingMapper<CommodityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CommodityDetailsMapper());
            this.MappingEngine.RegisterMap(new CommodityMappingMapper());      
            this.Container.RegisterType<IMapper<Commodity, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Commodity, RWEST.Nexus.MDM.Contracts.Commodity>, EnergyTrading.MDM.Mappers.CommodityMapper>();
        }
    }
}