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

    public class CommodityInstrumentTypeConfiguration : NexusEntityConfiguration<Services.CommodityInstrumentTypeService, MDM.CommodityInstrumentType, RWEST.Nexus.MDM.Contracts.CommodityInstrumentType, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityInstrumentType, MDM.CommodityInstrumentType>, EnergyTrading.MDM.Contracts.Mappers.CommodityInstrumentTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CommodityInstrumentTypeDetails, MDM.CommodityInstrumentType>, EnergyTrading.MDM.Contracts.Mappers.CommodityInstrumentTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CommodityInstrumentTypeMapping>, MappingMapper<CommodityInstrumentTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.CommodityInstrumentTypeDetailsMapper());
            MappingEngine.RegisterMap(new CommodityInstrumentTypeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.CommodityInstrumentType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.CommodityInstrumentType, RWEST.Nexus.MDM.Contracts.CommodityInstrumentType>, MDM.Mappers.CommodityInstrumentTypeMapper>();
        }
    }
}