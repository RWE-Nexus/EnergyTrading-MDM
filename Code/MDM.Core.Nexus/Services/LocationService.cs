namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class LocationService : MdmService<OpenNexus.MDM.Contracts.Location, Location, LocationMapping, Location, OpenNexus.MDM.Contracts.LocationDetails>
    {
        public LocationService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Location> Details(Location entity)
        {
            return new List<Location> { entity };
        }

        protected override IEnumerable<LocationMapping> Mappings(Location entity)
        {
            return entity.Mappings;
        }
    }
}