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

    using Agreement = EnergyTrading.MDM.Agreement;

    public class AgreementConfiguration : NexusEntityConfiguration<AgreementService, Agreement, RWEST.Nexus.MDM.Contracts.Agreement, 
        AgreementMapping, AgreementValidator>
    {
        public AgreementConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "agreement"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Agreement, Agreement>, EnergyTrading.MDM.Contracts.Mappers.AgreementMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.AgreementDetails, Agreement>, EnergyTrading.MDM.Contracts.Mappers.AgreementDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, AgreementMapping>, MappingMapper<AgreementMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.AgreementDetailsMapper());
            this.MappingEngine.RegisterMap(new AgreementMappingMapper());      
            this.Container.RegisterType<IMapper<Agreement, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Agreement, RWEST.Nexus.MDM.Contracts.Agreement>, EnergyTrading.MDM.Mappers.AgreementMapper>();
        }
    }
}