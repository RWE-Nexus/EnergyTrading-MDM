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

    public class ProductConfiguration : EntityConfiguration<Services.ProductService, MDM.Product, RWEST.Nexus.MDM.Contracts.Product, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Product, MDM.Product>, EnergyTrading.MDM.Contracts.Mappers.ProductMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductDetails, MDM.Product>, EnergyTrading.MDM.Contracts.Mappers.ProductDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductMapping>, MappingMapper<ProductMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ProductDetailsMapper());
            MappingEngine.RegisterMap(new ProductMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Product, List<Link>>, ProductLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Product, RWEST.Nexus.MDM.Contracts.Product>, MDM.Mappers.ProductMapper>();
        }
    }
}