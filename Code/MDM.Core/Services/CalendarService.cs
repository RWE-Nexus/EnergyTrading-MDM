namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

    public class CalendarService : MdmService<RWEST.Nexus.MDM.Contracts.Calendar, Calendar, CalendarMapping, Calendar, RWEST.Nexus.MDM.Contracts.CalendarDetails>
    {

        public CalendarService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<Calendar> Details(Calendar entity)
        {
            return new List<Calendar> { entity };
        }

        protected override IEnumerable<CalendarMapping> Mappings(Calendar entity)
        {
            return entity.Mappings;
        }
    }
}