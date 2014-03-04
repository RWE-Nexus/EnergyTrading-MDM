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

    using PortfolioHierarchy = EnergyTrading.MDM.PortfolioHierarchy;

    public class PortfolioHierarchyConfiguration : NexusEntityConfiguration<PortfolioHierarchyService, PortfolioHierarchy, RWEST.Nexus.MDM.Contracts.PortfolioHierarchy, 
		PortfolioHierarchyMapping, PortfolioHierarchyValidator>
    {
        public PortfolioHierarchyConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "portfoliohierarchy"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PortfolioHierarchy, PortfolioHierarchy>, EnergyTrading.MDM.Contracts.Mappers.PortfolioHierarchyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PortfolioHierarchyDetails, PortfolioHierarchy>, EnergyTrading.MDM.Contracts.Mappers.PortfolioHierarchyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PortfolioHierarchyMapping>, MappingMapper<PortfolioHierarchyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PortfolioHierarchyDetailsMapper());
            this.MappingEngine.RegisterMap(new PortfolioHierarchyMappingMapper());      
            this.Container.RegisterType<IMapper<PortfolioHierarchy, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<PortfolioHierarchy, RWEST.Nexus.MDM.Contracts.PortfolioHierarchy>, EnergyTrading.MDM.Mappers.PortfolioHierarchyMapper>();
        }
    }
}