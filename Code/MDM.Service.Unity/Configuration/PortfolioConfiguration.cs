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

    public class PortfolioConfiguration : EntityConfiguration<Services.PortfolioService, MDM.Portfolio, RWEST.Nexus.MDM.Contracts.Portfolio, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Portfolio, MDM.Portfolio>, EnergyTrading.MDM.Contracts.Mappers.PortfolioMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PortfolioDetails, MDM.Portfolio>, EnergyTrading.MDM.Contracts.Mappers.PortfolioDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PortfolioMapping>, MappingMapper<PortfolioMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PortfolioDetailsMapper());
            MappingEngine.RegisterMap(new PortfolioMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Portfolio, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Portfolio, RWEST.Nexus.MDM.Contracts.Portfolio>, MDM.Mappers.PortfolioMapper>();
        }
    }
}