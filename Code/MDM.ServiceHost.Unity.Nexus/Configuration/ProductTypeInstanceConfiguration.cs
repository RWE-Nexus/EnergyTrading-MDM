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

    using ProductTypeInstance = EnergyTrading.MDM.ProductTypeInstance;

    public class ProductTypeInstanceConfiguration : NexusEntityConfiguration<ProductTypeInstanceService, ProductTypeInstance, RWEST.Nexus.MDM.Contracts.ProductTypeInstance, 
		ProductTypeInstanceMapping, ProductTypeInstanceValidator>
    {
        public ProductTypeInstanceConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "producttypeinstance"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTypeInstance, ProductTypeInstance>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeInstanceMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTypeInstanceDetails, ProductTypeInstance>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeInstanceDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductTypeInstanceMapping>, MappingMapper<ProductTypeInstanceMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ProductTypeInstanceDetailsMapper());
            this.MappingEngine.RegisterMap(new ProductTypeInstanceMappingMapper());      
            this.Container.RegisterType<IMapper<ProductTypeInstance, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ProductTypeInstance, RWEST.Nexus.MDM.Contracts.ProductTypeInstance>, EnergyTrading.MDM.Mappers.ProductTypeInstanceMapper>();
        }
    }
}