namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;

    using Book = EnergyTrading.MDM.Book;
    using DateRange = EnergyTrading.DateRange;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class BookData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static BookData()
        {
            repository = ObjectScript.Repository;
        }

        public static Book CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Book>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Book CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Book>();

            var endurMapping = new BookMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = endur, 
                    IsDefault = true, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue.Subtract(new TimeSpan(72, 0, 0)))
                };

            entity.ProcessMapping(endurMapping);
            repository.Add(entity);
            repository.Flush();

            return entity;
        }

        public static RWEST.Nexus.MDM.Contracts.Book CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Book();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Book CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Book();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new BookMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new BookMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = endur, 
                    IsDefault = true, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            entity.ProcessMapping(trayportMapping);
            entity.ProcessMapping(endurMapping);

            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static void CreateSearch(Search search, Book entity1, Book entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Book MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Book currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Book contract)
        {
            var entity = ObjectMother.Create<Book>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new BookDetails()
                {
                    Name = entity.Name,
                };
        }

        private static void AddDetailsToEntity(Book entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<Book>();
            entity.Name = newEntity.Name;
        }

        private static void CreateSearchData(Search search, Book entity1, Book entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
