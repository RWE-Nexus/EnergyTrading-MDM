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
	

    public class TenorTypeConfiguration : NexusEntityConfiguration<Services.TenorTypeService, MDM.TenorType, RWEST.Nexus.MDM.Contracts.TenorType, 
		TenorTypeMapping, TenorTypeValidator>
    {
        public TenorTypeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "tenortype"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.TenorType, MDM.TenorType>, EnergyTrading.MDM.Contracts.Mappers.TenorTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.TenorTypeDetails, MDM.TenorType>, EnergyTrading.MDM.Contracts.Mappers.TenorTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, TenorTypeMapping>, MappingMapper<TenorTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.TenorTypeDetailsMapper());
            MappingEngine.RegisterMap(new TenorTypeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.TenorType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.TenorType, RWEST.Nexus.MDM.Contracts.TenorType>, MDM.Mappers.TenorTypeMapper>();
        }
    }
}