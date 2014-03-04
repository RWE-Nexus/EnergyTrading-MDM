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

    using TenorType = EnergyTrading.MDM.TenorType;

    public class TenorTypeConfiguration : NexusEntityConfiguration<TenorTypeService, TenorType, RWEST.Nexus.MDM.Contracts.TenorType, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.TenorType, TenorType>, EnergyTrading.MDM.Contracts.Mappers.TenorTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.TenorTypeDetails, TenorType>, EnergyTrading.MDM.Contracts.Mappers.TenorTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, TenorTypeMapping>, MappingMapper<TenorTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.TenorTypeDetailsMapper());
            this.MappingEngine.RegisterMap(new TenorTypeMappingMapper());      
            this.Container.RegisterType<IMapper<TenorType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<TenorType, RWEST.Nexus.MDM.Contracts.TenorType>, EnergyTrading.MDM.Mappers.TenorTypeMapper>();
        }
    }
}