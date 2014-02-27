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
	

    public class CommodityFeeTypeConfiguration : NexusEntityConfiguration<Services.CommodityFeeTypeService, MDM.CommodityFeeType, RWEST.Nexus.MDM.Contracts.CommodityFeeType, 
		CommodityFeeTypeMapping, CommodityFeeTypeValidator>
    {
        public CommodityFeeTypeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "commodityfeetype"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityFeeType, MDM.CommodityFeeType>, EnergyTrading.MDM.Contracts.Mappers.CommodityFeeTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityFeeTypeDetails, MDM.CommodityFeeType>, EnergyTrading.MDM.Contracts.Mappers.CommodityFeeTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CommodityFeeTypeMapping>, MappingMapper<CommodityFeeTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.CommodityFeeTypeDetailsMapper());
            MappingEngine.RegisterMap(new CommodityFeeTypeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.CommodityFeeType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.CommodityFeeType, RWEST.Nexus.MDM.Contracts.CommodityFeeType>, MDM.Mappers.CommodityFeeTypeMapper>();
        }
    }
}