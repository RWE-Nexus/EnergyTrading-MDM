namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class ProductTypeInstanceService : MdmService<RWEST.Nexus.MDM.Contracts.ProductTypeInstance, ProductTypeInstance, ProductTypeInstanceMapping, ProductTypeInstance, RWEST.Nexus.MDM.Contracts.ProductTypeInstanceDetails>
    {
        public ProductTypeInstanceService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<ProductTypeInstance> Details(ProductTypeInstance entity)
        {
            return new List<ProductTypeInstance> { entity };
        }

        protected override IEnumerable<ProductTypeInstanceMapping> Mappings(ProductTypeInstance entity)
        {
            return entity.Mappings;
        }
    }
}