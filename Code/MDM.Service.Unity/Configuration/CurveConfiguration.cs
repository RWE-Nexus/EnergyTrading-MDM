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

    public class CurveConfiguration : EntityConfiguration<Services.CurveService, MDM.Curve, RWEST.Nexus.MDM.Contracts.Curve, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Curve, MDM.Curve>, EnergyTrading.MDM.Contracts.Mappers.CurveMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CurveDetails, MDM.Curve>, EnergyTrading.MDM.Contracts.Mappers.CurveDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CurveMapping>, MappingMapper<CurveMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.CurveDetailsMapper());
            MappingEngine.RegisterMap(new CurveMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Curve, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Curve, RWEST.Nexus.MDM.Contracts.Curve>, MDM.Mappers.CurveMapper>();
        }
    }
}