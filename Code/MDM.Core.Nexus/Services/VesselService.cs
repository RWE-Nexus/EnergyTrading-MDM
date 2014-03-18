namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

			public class VesselService : MdmService<OpenNexus.MDM.Contracts.Vessel, Vessel, VesselMapping, Vessel, OpenNexus.MDM.Contracts.VesselDetails>
	    {

    public VesselService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Vessel> Details(Vessel entity)
        {
			return new List<Vessel> { entity };
	        }

        protected override IEnumerable<VesselMapping> Mappings(Vessel entity)
        {
            return entity.Mappings;
        }
    }
}