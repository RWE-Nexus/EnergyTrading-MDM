namespace EnergyTrading.Mdm.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    public class SourceSystemService : MdmService<EnergyTrading.Mdm.Contracts.SourceSystem, SourceSystem, SourceSystemMapping, SourceSystem, EnergyTrading.Mdm.Contracts.SourceSystemDetails>
    {
        public SourceSystemService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<SourceSystem> Details(SourceSystem entity)
        {
            return new List<SourceSystem> { entity };
        }

        protected override IEnumerable<SourceSystemMapping> Mappings(SourceSystem entity)
        {
            return entity.Mappings;
        }
    }
}