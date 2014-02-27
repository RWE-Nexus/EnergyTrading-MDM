namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class ShapeDayService : MdmService<RWEST.Nexus.MDM.Contracts.ShapeDay, ShapeDay, ShapeDayMapping, ShapeDay, RWEST.Nexus.MDM.Contracts.ShapeDayDetails>
	    {

    public ShapeDayService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<ShapeDay> Details(ShapeDay entity)
        {
			return new List<ShapeDay> { entity };
	        }

        protected override IEnumerable<ShapeDayMapping> Mappings(ShapeDay entity)
        {
            return entity.Mappings;
        }
    }
}