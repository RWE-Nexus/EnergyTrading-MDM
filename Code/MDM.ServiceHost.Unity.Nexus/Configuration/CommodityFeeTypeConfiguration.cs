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

    using CommodityFeeType = EnergyTrading.MDM.CommodityFeeType;

    public class CommodityFeeTypeConfiguration : NexusEntityConfiguration<CommodityFeeTypeService, CommodityFeeType, OpenNexus.MDM.Contracts.CommodityFeeType, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.CommodityFeeType, CommodityFeeType>, EnergyTrading.MDM.Contracts.Mappers.CommodityFeeTypeMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.CommodityFeeTypeDetails, CommodityFeeType>, EnergyTrading.MDM.Contracts.Mappers.CommodityFeeTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, CommodityFeeTypeMapping>, MappingMapper<CommodityFeeTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CommodityFeeTypeDetailsMapper());
            this.MappingEngine.RegisterMap(new CommodityFeeTypeMappingMapper());      
            this.Container.RegisterType<IMapper<CommodityFeeType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<CommodityFeeType, OpenNexus.MDM.Contracts.CommodityFeeType>, EnergyTrading.MDM.Mappers.CommodityFeeTypeMapper>();
        }
    }
}