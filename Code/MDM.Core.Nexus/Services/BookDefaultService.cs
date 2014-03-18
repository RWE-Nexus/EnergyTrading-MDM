namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

            public class BookDefaultService : MdmService<OpenNexus.MDM.Contracts.BookDefault, BookDefault, BookDefaultMapping, BookDefault, OpenNexus.MDM.Contracts.BookDefaultDetails>
        {

                public BookDefaultService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache)
                    : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

                protected override IEnumerable<BookDefault> Details(BookDefault entity)
        {
            return new List<BookDefault> { entity };
            }

                protected override IEnumerable<BookDefaultMapping> Mappings(BookDefault entity)
        {
            return entity.Mappings;
        }
    }
}