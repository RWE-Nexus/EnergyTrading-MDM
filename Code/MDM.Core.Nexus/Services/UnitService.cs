namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class UnitService : MdmService<OpenNexus.MDM.Contracts.Unit, Unit, UnitMapping, Unit, OpenNexus.MDM.Contracts.UnitDetails>
	    {

    public UnitService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Unit> Details(Unit entity)
        {
			return new List<Unit> { entity };
	        }

        protected override IEnumerable<UnitMapping> Mappings(Unit entity)
        {
            return entity.Mappings;
        }
    }
}