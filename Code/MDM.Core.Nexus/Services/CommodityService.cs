namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class CommodityService : MdmService<OpenNexus.MDM.Contracts.Commodity, Commodity, CommodityMapping, Commodity, OpenNexus.MDM.Contracts.CommodityDetails>
    {
        public CommodityService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Commodity> Details(Commodity entity)
        {
            return new List<Commodity> { entity };
        }

        protected override IEnumerable<CommodityMapping> Mappings(Commodity entity)
        {
            return entity.Mappings;
        }
    }
}