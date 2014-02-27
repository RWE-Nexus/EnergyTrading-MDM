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

    public class ProductTypeConfiguration : NexusEntityConfiguration<Services.ProductTypeService, MDM.ProductType, RWEST.Nexus.MDM.Contracts.ProductType, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductType, MDM.ProductType>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTypeDetails, MDM.ProductType>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductTypeMapping>, MappingMapper<ProductTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ProductTypeDetailsMapper());
            MappingEngine.RegisterMap(new ProductTypeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ProductType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ProductType, RWEST.Nexus.MDM.Contracts.ProductType>, MDM.Mappers.ProductTypeMapper>();
        }
    }
}