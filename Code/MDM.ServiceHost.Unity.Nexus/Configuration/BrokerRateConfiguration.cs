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

    using BrokerRate = EnergyTrading.MDM.BrokerRate;
    using BrokerRateDetails = EnergyTrading.MDM.BrokerRateDetails;

    public class BrokerRateConfiguration : NexusEntityConfiguration<BrokerRateService, BrokerRate, RWEST.Nexus.MDM.Contracts.BrokerRate, BrokerRateMapping, BrokerRateValidator>
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerRate, BrokerRate>, EnergyTrading.MDM.Contracts.Mappers.BrokerRateMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BrokerRateDetails, BrokerRateDetails>, EnergyTrading.MDM.Contracts.Mappers.BrokerRateDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, BrokerRateMapping>, MappingMapper<BrokerRateMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.BrokerRateDetailsMapper());
            this.MappingEngine.RegisterMap(new BrokerRateMappingMapper());      
            this.Container.RegisterType<IMapper<BrokerRate, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<BrokerRate, RWEST.Nexus.MDM.Contracts.BrokerRate>, EnergyTrading.MDM.Mappers.BrokerRateMapper>();
        }
    }
}