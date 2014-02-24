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
	

    public class ProductTenorTypeConfiguration : EntityConfiguration<Services.ProductTenorTypeService, MDM.ProductTenorType, RWEST.Nexus.MDM.Contracts.ProductTenorType, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTenorType, MDM.ProductTenorType>, EnergyTrading.MDM.Contracts.Mappers.ProductTenorTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTenorTypeDetails, MDM.ProductTenorType>, EnergyTrading.MDM.Contracts.Mappers.ProductTenorTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductTenorTypeMapping>, MappingMapper<ProductTenorTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ProductTenorTypeDetailsMapper());
            MappingEngine.RegisterMap(new ProductTenorTypeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ProductTenorType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ProductTenorType, RWEST.Nexus.MDM.Contracts.ProductTenorType>, MDM.Mappers.ProductTenorTypeMapper>();
        }
    }
}