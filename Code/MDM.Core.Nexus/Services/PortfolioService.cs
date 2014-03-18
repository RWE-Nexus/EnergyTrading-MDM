namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class PortfolioService : MdmService<OpenNexus.MDM.Contracts.Portfolio, Portfolio, PortfolioMapping, Portfolio, OpenNexus.MDM.Contracts.PortfolioDetails>
    {
        public PortfolioService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Portfolio> Details(Portfolio entity)
        {
            return new List<Portfolio> { entity };
        }

        protected override IEnumerable<PortfolioMapping> Mappings(Portfolio entity)
        {
            return entity.Mappings;
        }
    }
}