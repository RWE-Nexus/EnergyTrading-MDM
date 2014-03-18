namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class PortfolioHierarchyService : MdmService<OpenNexus.MDM.Contracts.PortfolioHierarchy, PortfolioHierarchy, PortfolioHierarchyMapping, PortfolioHierarchy, OpenNexus.MDM.Contracts.PortfolioHierarchyDetails>
    {
        public PortfolioHierarchyService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<PortfolioHierarchy> Details(PortfolioHierarchy entity)
        {
            return new List<PortfolioHierarchy> { entity };
        }

        protected override IEnumerable<PortfolioHierarchyMapping> Mappings(PortfolioHierarchy entity)
        {
            return entity.Mappings;
        }
    }
}