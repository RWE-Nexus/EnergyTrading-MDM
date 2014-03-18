namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class LocationRoleService : MdmService<OpenNexus.MDM.Contracts.LocationRole, LocationRole, LocationRoleMapping, LocationRole, OpenNexus.MDM.Contracts.LocationRoleDetails>
    {
        public LocationRoleService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<LocationRole> Details(LocationRole entity)
        {
            return new List<LocationRole> { entity };
        }

        protected override IEnumerable<LocationRoleMapping> Mappings(LocationRole entity)
        {
            return entity.Mappings;
        }
    }
}