using System.Collections.Generic;

namespace EnergyTrading.MDM.Configuration
{
    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;

    public class ProductCurveConfiguration : EntityConfiguration<Services.ProductCurveService, MDM.ProductCurve, RWEST.Nexus.MDM.Contracts.ProductCurve, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductCurve, MDM.ProductCurve>, EnergyTrading.MDM.Contracts.Mappers.ProductCurveMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ProductCurveDetails, MDM.ProductCurve>, EnergyTrading.MDM.Contracts.Mappers.ProductCurveDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ProductCurveMapping>, MappingMapper<ProductCurveMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ProductCurveDetailsMapper());
            MappingEngine.RegisterMap(new ProductCurveMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ProductCurve, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ProductCurve, RWEST.Nexus.MDM.Contracts.ProductCurve>, MDM.Mappers.ProductCurveMapper>();
        }
    }
}