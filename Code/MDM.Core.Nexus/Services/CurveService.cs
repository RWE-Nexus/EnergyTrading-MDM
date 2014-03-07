namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class CurveService : MdmService<RWEST.Nexus.MDM.Contracts.Curve, Curve, CurveMapping, Curve, RWEST.Nexus.MDM.Contracts.CurveDetails>
    {
        public CurveService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Curve> Details(Curve entity)
        {
            return new List<Curve> { entity };
        }

        protected override IEnumerable<CurveMapping> Mappings(Curve entity)
        {
            return entity.Mappings;
        }
    }
}