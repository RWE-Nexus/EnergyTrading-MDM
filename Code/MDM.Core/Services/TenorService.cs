namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class TenorService : MdmService<RWEST.Nexus.MDM.Contracts.Tenor, Tenor, TenorMapping, Tenor, RWEST.Nexus.MDM.Contracts.TenorDetails>
	    {

    public TenorService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Tenor> Details(Tenor entity)
        {
			return new List<Tenor> { entity };
	        }

        protected override IEnumerable<TenorMapping> Mappings(Tenor entity)
        {
            return entity.Mappings;
        }
    }
}