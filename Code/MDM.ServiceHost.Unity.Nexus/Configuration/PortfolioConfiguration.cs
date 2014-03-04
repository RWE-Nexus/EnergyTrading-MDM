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

    using Portfolio = EnergyTrading.MDM.Portfolio;

    public class PortfolioConfiguration : NexusEntityConfiguration<PortfolioService, Portfolio, RWEST.Nexus.MDM.Contracts.Portfolio, 
		PortfolioMapping, PortfolioValidator>
    {
        public PortfolioConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "portfolio"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Portfolio, Portfolio>, EnergyTrading.MDM.Contracts.Mappers.PortfolioMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PortfolioDetails, Portfolio>, EnergyTrading.MDM.Contracts.Mappers.PortfolioDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PortfolioMapping>, MappingMapper<PortfolioMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PortfolioDetailsMapper());
            this.MappingEngine.RegisterMap(new PortfolioMappingMapper());      
            this.Container.RegisterType<IMapper<Portfolio, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Portfolio, RWEST.Nexus.MDM.Contracts.Portfolio>, EnergyTrading.MDM.Mappers.PortfolioMapper>();
        }
    }
}