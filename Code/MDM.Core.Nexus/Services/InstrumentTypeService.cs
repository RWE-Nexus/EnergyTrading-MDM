namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class InstrumentTypeService : MdmService<OpenNexus.MDM.Contracts.InstrumentType, InstrumentType, InstrumentTypeMapping, InstrumentType, OpenNexus.MDM.Contracts.InstrumentTypeDetails>
    {
        public InstrumentTypeService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<InstrumentType> Details(InstrumentType entity)
        {
            return new List<InstrumentType> { entity };
        }

        protected override IEnumerable<InstrumentTypeMapping> Mappings(InstrumentType entity)
        {
            return entity.Mappings;
        }
    }
}