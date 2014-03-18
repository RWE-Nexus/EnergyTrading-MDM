namespace EnergyTrading.MDM.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.Validation;

            public class BookService : MdmService<OpenNexus.MDM.Contracts.Book, Book, BookMapping, Book, OpenNexus.MDM.Contracts.BookDetails>
        {

    public BookService(IValidatorEngine validatorFactory, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache) : base(validatorFactory, mappingEngine, repository, searchCache)
    {
    }

        protected override IEnumerable<Book> Details(Book entity)
        {
            return new List<Book> { entity };
            }

        protected override IEnumerable<BookMapping> Mappings(Book entity)
        {
            return entity.Mappings;
        }
    }
}