namespace EnergyTrading.MDM.Configuration
{
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;
    

    public class AgreementConfiguration : EntityConfiguration<Services.AgreementService, MDM.Agreement, RWEST.Nexus.MDM.Contracts.Agreement, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Agreement, MDM.Agreement>, EnergyTrading.MDM.Contracts.Mappers.AgreementMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.AgreementDetails, MDM.Agreement>, EnergyTrading.MDM.Contracts.Mappers.AgreementDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, AgreementMapping>, MappingMapper<AgreementMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.AgreementDetailsMapper());
            MappingEngine.RegisterMap(new AgreementMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Agreement, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Agreement, RWEST.Nexus.MDM.Contracts.Agreement>, MDM.Mappers.AgreementMapper>();
        }
    }
}