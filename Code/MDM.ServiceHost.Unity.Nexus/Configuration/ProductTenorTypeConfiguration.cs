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

    using ProductTenorType = EnergyTrading.MDM.ProductTenorType;

    public class ProductTenorTypeConfiguration : NexusEntityConfiguration<ProductTenorTypeService, ProductTenorType, OpenNexus.MDM.Contracts.ProductTenorType, 
		ProductTenorTypeMapping, ProductTenorTypeValidator>
    {
        public ProductTenorTypeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "producttenortype"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ProductTenorType, ProductTenorType>, EnergyTrading.MDM.Contracts.Mappers.ProductTenorTypeMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ProductTenorTypeDetails, ProductTenorType>, EnergyTrading.MDM.Contracts.Mappers.ProductTenorTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, ProductTenorTypeMapping>, MappingMapper<ProductTenorTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ProductTenorTypeDetailsMapper());
            this.MappingEngine.RegisterMap(new ProductTenorTypeMappingMapper());      
            this.Container.RegisterType<IMapper<ProductTenorType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ProductTenorType, OpenNexus.MDM.Contracts.ProductTenorType>, EnergyTrading.MDM.Mappers.ProductTenorTypeMapper>();
        }
    }
}