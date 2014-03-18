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

    using Product = EnergyTrading.MDM.Product;

    public class ProductConfiguration : NexusEntityConfiguration<ProductService, Product, OpenNexus.MDM.Contracts.Product, 
		ProductMapping, ProductValidator>
    {
        public ProductConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "product"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Product, Product>, EnergyTrading.MDM.Contracts.Mappers.ProductMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ProductDetails, Product>, EnergyTrading.MDM.Contracts.Mappers.ProductDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, ProductMapping>, MappingMapper<ProductMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ProductDetailsMapper());
            this.MappingEngine.RegisterMap(new ProductMappingMapper());      
            this.Container.RegisterType<IMapper<Product, List<Link>>, ProductLinksMapper>();
            this.Container.RegisterType<IMapper<Product, OpenNexus.MDM.Contracts.Product>, EnergyTrading.MDM.Mappers.ProductMapper>();
        }
    }
}