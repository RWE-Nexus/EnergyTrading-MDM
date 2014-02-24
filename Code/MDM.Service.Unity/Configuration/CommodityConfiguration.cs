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

    public class CommodityConfiguration : EntityConfiguration<Services.CommodityService, MDM.Commodity, RWEST.Nexus.MDM.Contracts.Commodity, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Commodity, MDM.Commodity>, EnergyTrading.MDM.Contracts.Mappers.CommodityMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityDetails, MDM.Commodity>, EnergyTrading.MDM.Contracts.Mappers.CommodityDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CommodityMapping>, MappingMapper<CommodityMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.CommodityDetailsMapper());
            MappingEngine.RegisterMap(new CommodityMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Commodity, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Commodity, RWEST.Nexus.MDM.Contracts.Commodity>, MDM.Mappers.CommodityMapper>();
        }
    }
}