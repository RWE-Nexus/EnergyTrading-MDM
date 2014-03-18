namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class HierarchyService : MdmService<OpenNexus.MDM.Contracts.Hierarchy, Hierarchy, HierarchyMapping, Hierarchy, OpenNexus.MDM.Contracts.HierarchyDetails>
    {
        public HierarchyService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Hierarchy> Details(Hierarchy entity)
        {
            return new List<Hierarchy> { entity };
        }

        protected override IEnumerable<HierarchyMapping> Mappings(Hierarchy entity)
        {
            return entity.Mappings;
        }
    }
}