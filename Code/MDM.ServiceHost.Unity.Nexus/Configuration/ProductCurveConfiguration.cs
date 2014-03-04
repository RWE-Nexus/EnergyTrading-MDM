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

    using ProductCurve = EnergyTrading.MDM.ProductCurve;

    public class ProductCurveConfiguration : NexusEntityConfiguration<ProductCurveService, ProductCurve, RWEST.Nexus.MDM.Contracts.ProductCurve, 
		ProductCurveMapping, ProductCurveValidator>
    {
        public ProductCurveConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "productcurve"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductCurve, ProductCurve>, EnergyTrading.MDM.Contracts.Mappers.ProductCurveMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductCurveDetails, ProductCurve>, EnergyTrading.MDM.Contracts.Mappers.ProductCurveDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductCurveMapping>, MappingMapper<ProductCurveMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ProductCurveDetailsMapper());
            this.MappingEngine.RegisterMap(new ProductCurveMappingMapper());      
            this.Container.RegisterType<IMapper<ProductCurve, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ProductCurve, RWEST.Nexus.MDM.Contracts.ProductCurve>, EnergyTrading.MDM.Mappers.ProductCurveMapper>();
        }
    }
}