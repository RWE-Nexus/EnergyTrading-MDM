namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class DimensionService : MdmService<OpenNexus.MDM.Contracts.Dimension, Dimension, DimensionMapping, Dimension, OpenNexus.MDM.Contracts.DimensionDetails>
	    {

    public DimensionService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Dimension> Details(Dimension entity)
        {
			return new List<Dimension> { entity };
	        }

        protected override IEnumerable<DimensionMapping> Mappings(Dimension entity)
        {
            return entity.Mappings;
        }
    }
}