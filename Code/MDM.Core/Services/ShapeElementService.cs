namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class ShapeElementService : MdmService<RWEST.Nexus.MDM.Contracts.ShapeElement, ShapeElement, ShapeElementMapping, ShapeElement, RWEST.Nexus.MDM.Contracts.ShapeElementDetails>
	    {

    public ShapeElementService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<ShapeElement> Details(ShapeElement entity)
        {
			return new List<ShapeElement> { entity };
	        }

        protected override IEnumerable<ShapeElementMapping> Mappings(ShapeElement entity)
        {
            return entity.Mappings;
        }
    }
}