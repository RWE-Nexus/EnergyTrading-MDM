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

    using CommodityInstrumentType = EnergyTrading.MDM.CommodityInstrumentType;

    public class CommodityInstrumentTypeConfiguration : NexusEntityConfiguration<CommodityInstrumentTypeService, CommodityInstrumentType, RWEST.Nexus.MDM.Contracts.CommodityInstrumentType, 
		CommodityInstrumentTypeMapping, CommodityInstrumentTypeValidator>
    {
        public CommodityInstrumentTypeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "commodityinstrumenttype"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityInstrumentType, CommodityInstrumentType>, EnergyTrading.MDM.Contracts.Mappers.CommodityInstrumentTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityInstrumentTypeDetails, CommodityInstrumentType>, EnergyTrading.MDM.Contracts.Mappers.CommodityInstrumentTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CommodityInstrumentTypeMapping>, MappingMapper<CommodityInstrumentTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CommodityInstrumentTypeDetailsMapper());
            this.MappingEngine.RegisterMap(new CommodityInstrumentTypeMappingMapper());      
            this.Container.RegisterType<IMapper<CommodityInstrumentType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<CommodityInstrumentType, RWEST.Nexus.MDM.Contracts.CommodityInstrumentType>, EnergyTrading.MDM.Mappers.CommodityInstrumentTypeMapper>();
        }
    }
}