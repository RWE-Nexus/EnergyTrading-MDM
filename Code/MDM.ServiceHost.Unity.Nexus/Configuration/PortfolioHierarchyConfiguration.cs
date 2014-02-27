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

    public class PortfolioHierarchyConfiguration : NexusEntityConfiguration<Services.PortfolioHierarchyService, MDM.PortfolioHierarchy, RWEST.Nexus.MDM.Contracts.PortfolioHierarchy, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PortfolioHierarchy, MDM.PortfolioHierarchy>, EnergyTrading.MDM.Contracts.Mappers.PortfolioHierarchyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PortfolioHierarchyDetails, MDM.PortfolioHierarchy>, EnergyTrading.MDM.Contracts.Mappers.PortfolioHierarchyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PortfolioHierarchyMapping>, MappingMapper<PortfolioHierarchyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PortfolioHierarchyDetailsMapper());
            MappingEngine.RegisterMap(new PortfolioHierarchyMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.PortfolioHierarchy, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.PortfolioHierarchy, RWEST.Nexus.MDM.Contracts.PortfolioHierarchy>, MDM.Mappers.PortfolioHierarchyMapper>();
        }
    }
}