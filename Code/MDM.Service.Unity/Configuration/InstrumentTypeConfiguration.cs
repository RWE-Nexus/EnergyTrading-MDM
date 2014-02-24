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

    public class InstrumentTypeConfiguration : EntityConfiguration<Services.InstrumentTypeService, MDM.InstrumentType, RWEST.Nexus.MDM.Contracts.InstrumentType, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.InstrumentType, MDM.InstrumentType>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.InstrumentTypeDetails, MDM.InstrumentType>, EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, InstrumentTypeMapping>, MappingMapper<InstrumentTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.InstrumentTypeDetailsMapper());
            MappingEngine.RegisterMap(new InstrumentTypeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.InstrumentType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.InstrumentType, RWEST.Nexus.MDM.Contracts.InstrumentType>, MDM.Mappers.InstrumentTypeMapper>();
        }
    }
}