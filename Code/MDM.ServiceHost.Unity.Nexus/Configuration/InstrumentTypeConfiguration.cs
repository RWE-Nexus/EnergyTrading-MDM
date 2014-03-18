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

    using InstrumentType = EnergyTrading.MDM.InstrumentType;

    public class InstrumentTypeConfiguration : NexusEntityConfiguration<InstrumentTypeService, InstrumentType, OpenNexus.MDM.Contracts.InstrumentType, 
		InstrumentTypeMapping, InstrumentTypeValidator>
    {
        public InstrumentTypeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "instrumenttype"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.InstrumentType, InstrumentType>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.InstrumentTypeDetails, InstrumentType>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, InstrumentTypeMapping>, MappingMapper<InstrumentTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.InstrumentTypeDetailsMapper());
            this.MappingEngine.RegisterMap(new InstrumentTypeMappingMapper());      
            this.Container.RegisterType<IMapper<InstrumentType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<InstrumentType, OpenNexus.MDM.Contracts.InstrumentType>, EnergyTrading.MDM.Mappers.InstrumentTypeMapper>();
        }
    }
}