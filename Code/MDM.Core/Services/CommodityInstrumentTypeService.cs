namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class CommodityInstrumentTypeService : MdmService<RWEST.Nexus.MDM.Contracts.CommodityInstrumentType, CommodityInstrumentType, CommodityInstrumentTypeMapping, CommodityInstrumentType, RWEST.Nexus.MDM.Contracts.CommodityInstrumentTypeDetails>
    {
        public CommodityInstrumentTypeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<CommodityInstrumentType> Details(CommodityInstrumentType entity)
        {
            return new List<CommodityInstrumentType> { entity };
        }

        protected override IEnumerable<CommodityInstrumentTypeMapping> Mappings(CommodityInstrumentType entity)
        {
            return entity.Mappings;
        }
    }
}