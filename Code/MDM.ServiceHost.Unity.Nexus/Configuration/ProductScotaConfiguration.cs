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

    using ProductScota = EnergyTrading.MDM.ProductScota;

    public class ProductScotaConfiguration : NexusEntityConfiguration<ProductScotaService, ProductScota, RWEST.Nexus.MDM.Contracts.ProductScota, 
		ProductScotaMapping, ProductScotaValidator>
    {
        public ProductScotaConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "productscota"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductScota, ProductScota>, EnergyTrading.MDM.Contracts.Mappers.ProductScotaMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductScotaDetails, ProductScota>, EnergyTrading.MDM.Contracts.Mappers.ProductScotaDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductScotaMapping>, MappingMapper<ProductScotaMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ProductScotaDetailsMapper());
            this.MappingEngine.RegisterMap(new ProductScotaMappingMapper());      
            this.Container.RegisterType<IMapper<ProductScota, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ProductScota, RWEST.Nexus.MDM.Contracts.ProductScota>, EnergyTrading.MDM.Mappers.ProductScotaMapper>();
        }
    }
}