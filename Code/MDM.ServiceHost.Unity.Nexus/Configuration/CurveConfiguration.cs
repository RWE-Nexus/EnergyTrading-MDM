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

    using Curve = EnergyTrading.MDM.Curve;

    public class CurveConfiguration : NexusEntityConfiguration<CurveService, Curve, RWEST.Nexus.MDM.Contracts.Curve, 
		CurveMapping, CurveValidator>
    {
        public CurveConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "curve"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Curve, Curve>, EnergyTrading.MDM.Contracts.Mappers.CurveMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CurveDetails, Curve>, EnergyTrading.MDM.Contracts.Mappers.CurveDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CurveMapping>, MappingMapper<CurveMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CurveDetailsMapper());
            this.MappingEngine.RegisterMap(new CurveMappingMapper());      
            this.Container.RegisterType<IMapper<Curve, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Curve, RWEST.Nexus.MDM.Contracts.Curve>, EnergyTrading.MDM.Mappers.CurveMapper>();
        }
    }
}