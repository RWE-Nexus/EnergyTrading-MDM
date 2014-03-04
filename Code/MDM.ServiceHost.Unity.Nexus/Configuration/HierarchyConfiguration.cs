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

    using Hierarchy = EnergyTrading.MDM.Hierarchy;

    public class HierarchyConfiguration : NexusEntityConfiguration<HierarchyService, Hierarchy, RWEST.Nexus.MDM.Contracts.Hierarchy, 
		HierarchyMapping, HierarchyValidator>
    {
        public HierarchyConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "hierarchy"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Hierarchy, Hierarchy>, EnergyTrading.MDM.Contracts.Mappers.HierarchyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.HierarchyDetails, Hierarchy>, EnergyTrading.MDM.Contracts.Mappers.HierarchyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, HierarchyMapping>, MappingMapper<HierarchyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.HierarchyDetailsMapper());
            this.MappingEngine.RegisterMap(new HierarchyMappingMapper());      
            this.Container.RegisterType<IMapper<Hierarchy, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Hierarchy, RWEST.Nexus.MDM.Contracts.Hierarchy>, EnergyTrading.MDM.Mappers.HierarchyMapper>();
        }
    }
}