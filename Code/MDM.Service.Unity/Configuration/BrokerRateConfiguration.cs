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

    public class BrokerRateConfiguration : EntityConfiguration<Services.BrokerRateService, MDM.BrokerRate, RWEST.Nexus.MDM.Contracts.BrokerRate, BrokerRateMapping, BrokerRateValidator>
    {
        public BrokerRateConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "brokerrate"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerRate, MDM.BrokerRate>, EnergyTrading.MDM.Contracts.Mappers.BrokerRateMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerRateDetails, MDM.BrokerRateDetails>, EnergyTrading.MDM.Contracts.Mappers.BrokerRateDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, BrokerRateMapping>, MappingMapper<BrokerRateMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.BrokerRateDetailsMapper());
            MappingEngine.RegisterMap(new BrokerRateMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.BrokerRate, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.BrokerRate, RWEST.Nexus.MDM.Contracts.BrokerRate>, MDM.Mappers.BrokerRateMapper>();
        }
    }
}