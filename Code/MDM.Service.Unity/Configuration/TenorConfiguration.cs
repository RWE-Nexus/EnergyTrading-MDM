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
	

    public class TenorConfiguration : EntityConfiguration<Services.TenorService, MDM.Tenor, RWEST.Nexus.MDM.Contracts.Tenor, 
		TenorMapping, TenorValidator>
    {
        public TenorConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "tenor"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Tenor, MDM.Tenor>, EnergyTrading.MDM.Contracts.Mappers.TenorMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.TenorDetails, MDM.Tenor>, EnergyTrading.MDM.Contracts.Mappers.TenorDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, TenorMapping>, MappingMapper<TenorMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.TenorDetailsMapper());
            MappingEngine.RegisterMap(new TenorMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Tenor, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Tenor, RWEST.Nexus.MDM.Contracts.Tenor>, MDM.Mappers.TenorMapper>();
        }
    }
}