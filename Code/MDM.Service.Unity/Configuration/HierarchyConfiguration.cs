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

    public class HierarchyConfiguration : EntityConfiguration<Services.HierarchyService, MDM.Hierarchy, RWEST.Nexus.MDM.Contracts.Hierarchy, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Hierarchy, MDM.Hierarchy>, EnergyTrading.MDM.Contracts.Mappers.HierarchyMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.HierarchyDetails, MDM.Hierarchy>, EnergyTrading.MDM.Contracts.Mappers.HierarchyDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, HierarchyMapping>, MappingMapper<HierarchyMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.HierarchyDetailsMapper());
            MappingEngine.RegisterMap(new HierarchyMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Hierarchy, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Hierarchy, RWEST.Nexus.MDM.Contracts.Hierarchy>, MDM.Mappers.HierarchyMapper>();
        }
    }
}