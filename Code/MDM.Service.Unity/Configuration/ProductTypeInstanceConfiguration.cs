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

    public class ProductTypeInstanceConfiguration : EntityConfiguration<Services.ProductTypeInstanceService, MDM.ProductTypeInstance, RWEST.Nexus.MDM.Contracts.ProductTypeInstance, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTypeInstance, MDM.ProductTypeInstance>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeInstanceMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductTypeInstanceDetails, MDM.ProductTypeInstance>, EnergyTrading.MDM.Contracts.Mappers.ProductTypeInstanceDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductTypeInstanceMapping>, MappingMapper<ProductTypeInstanceMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ProductTypeInstanceDetailsMapper());
            MappingEngine.RegisterMap(new ProductTypeInstanceMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ProductTypeInstance, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ProductTypeInstance, RWEST.Nexus.MDM.Contracts.ProductTypeInstance>, MDM.Mappers.ProductTypeInstanceMapper>();
        }
    }
}