namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class ShipperCodeService : MdmService<RWEST.Nexus.MDM.Contracts.ShipperCode, ShipperCode, ShipperCodeMapping, ShipperCode, RWEST.Nexus.MDM.Contracts.ShipperCodeDetails>
    {
        public ShipperCodeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<ShipperCode> Details(ShipperCode entity)
        {
            return new List<ShipperCode> { entity };
        }

        protected override IEnumerable<ShipperCodeMapping> Mappings(ShipperCode entity)
        {
            return entity.Mappings;
        }
    }
}