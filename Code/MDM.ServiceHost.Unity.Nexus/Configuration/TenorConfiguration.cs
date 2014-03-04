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

    using Tenor = EnergyTrading.MDM.Tenor;

    public class TenorConfiguration : NexusEntityConfiguration<TenorService, Tenor, RWEST.Nexus.MDM.Contracts.Tenor, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Tenor, Tenor>, EnergyTrading.MDM.Contracts.Mappers.TenorMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.TenorDetails, Tenor>, EnergyTrading.MDM.Contracts.Mappers.TenorDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, TenorMapping>, MappingMapper<TenorMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.TenorDetailsMapper());
            this.MappingEngine.RegisterMap(new TenorMappingMapper());      
            this.Container.RegisterType<IMapper<Tenor, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Tenor, RWEST.Nexus.MDM.Contracts.Tenor>, EnergyTrading.MDM.Mappers.TenorMapper>();
        }
    }
}