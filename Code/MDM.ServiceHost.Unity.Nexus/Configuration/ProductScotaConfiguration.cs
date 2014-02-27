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
	

    public class ProductScotaConfiguration : NexusEntityConfiguration<Services.ProductScotaService, MDM.ProductScota, RWEST.Nexus.MDM.Contracts.ProductScota, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductScota, MDM.ProductScota>, EnergyTrading.MDM.Contracts.Mappers.ProductScotaMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductScotaDetails, MDM.ProductScota>, EnergyTrading.MDM.Contracts.Mappers.ProductScotaDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductScotaMapping>, MappingMapper<ProductScotaMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ProductScotaDetailsMapper());
            MappingEngine.RegisterMap(new ProductScotaMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ProductScota, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ProductScota, RWEST.Nexus.MDM.Contracts.ProductScota>, MDM.Mappers.ProductScotaMapper>();
        }
    }
}