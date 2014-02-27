namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class TenorTypeService : MdmService<RWEST.Nexus.MDM.Contracts.TenorType, TenorType, TenorTypeMapping, TenorType, RWEST.Nexus.MDM.Contracts.TenorTypeDetails>
	    {

    public TenorTypeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<TenorType> Details(TenorType entity)
        {
			return new List<TenorType> { entity };
	        }

        protected override IEnumerable<TenorTypeMapping> Mappings(TenorType entity)
        {
            return entity.Mappings;
        }
    }
}