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

    using ProductScota = EnergyTrading.MDM.ProductScota;

    public class ProductScotaConfiguration : NexusEntityConfiguration<ProductScotaService, ProductScota, OpenNexus.MDM.Contracts.ProductScota, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ProductScota, ProductScota>, EnergyTrading.MDM.Contracts.Mappers.ProductScotaMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ProductScotaDetails, ProductScota>, EnergyTrading.MDM.Contracts.Mappers.ProductScotaDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, ProductScotaMapping>, MappingMapper<ProductScotaMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ProductScotaDetailsMapper());
            this.MappingEngine.RegisterMap(new ProductScotaMappingMapper());      
            this.Container.RegisterType<IMapper<ProductScota, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ProductScota, OpenNexus.MDM.Contracts.ProductScota>, EnergyTrading.MDM.Mappers.ProductScotaMapper>();
        }
    }
}