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

    using ProductType = EnergyTrading.MDM.ProductType;

    public class ProductTypeConfiguration : NexusEntityConfiguration<ProductTypeService, ProductType, RWEST.Nexus.MDM.Contracts.ProductType, 
		ProductTypeMapping, ProductTypeValidator>
    {
        public ProductTypeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "producttype"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductType, ProductType>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTypeDetails, ProductType>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductTypeMapping>, MappingMapper<ProductTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ProductTypeDetailsMapper());
            this.MappingEngine.RegisterMap(new ProductTypeMappingMapper());      
            this.Container.RegisterType<IMapper<ProductType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ProductType, RWEST.Nexus.MDM.Contracts.ProductType>, EnergyTrading.MDM.Mappers.ProductTypeMapper>();
        }
    }
}