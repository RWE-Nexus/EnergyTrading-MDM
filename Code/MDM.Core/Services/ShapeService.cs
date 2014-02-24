namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class ShapeService : MdmService<RWEST.Nexus.MDM.Contracts.Shape, Shape, ShapeMapping, Shape, RWEST.Nexus.MDM.Contracts.ShapeDetails>
    {

        public ShapeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Shape> Details(Shape entity)
        {
            return new List<Shape> { entity };
        }

        protected override IEnumerable<ShapeMapping> Mappings(Shape entity)
        {
            return entity.Mappings;
        }
    }
}