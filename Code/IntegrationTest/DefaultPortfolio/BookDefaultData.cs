namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    using DateRange = EnergyTrading.DateRange;
    using BookDefault = EnergyTrading.MDM.BookDefault;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class BookDefaultData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static BookDefaultData()
        {
            repository = ObjectScript.Repository;
        }

        public static BookDefault CreateBasicEntity()
        {
            var entity = ObjectMother.Create<BookDefault>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static BookDefault CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<BookDefault>();

            var endurMapping = new BookDefaultMapping
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

        public static RWEST.Nexus.MDM.Contracts.BookDefault CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.BookDefault();
            AddDetailsToContract(contract);
            return contract;
        }

        public static BookDefault CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new BookDefault();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new BookDefaultMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new BookDefaultMapping
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

        public static void CreateSearch(Search search, BookDefault entity1, BookDefault entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.BookDefault MakeChangeToContract(RWEST.Nexus.MDM.Contracts.BookDefault currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.BookDefault contract)
        {
            var entity = ObjectMother.Create<BookDefault>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new BookDefaultDetails()
                {
                    Name = entity.Name,
                    Desk = entity.Desk.CreateNexusEntityId(() => entity.Desk.LatestDetails.Name),
                    Trader = entity.Trader.CreateNexusEntityId(() => entity.Trader.LatestDetails.FirstName + " " + entity.Trader.LatestDetails.LastName),
                    Book = entity.Book.CreateNexusEntityId(() => entity.Book.Name),
                    GfProductMapping = entity.GfProductMapping,
                    DefaultType = entity.DefaultType,
                    PartyRole = entity.PartyRole.CreateNexusEntityId(() => entity.PartyRole.LatestDetails.Name)
                };
        }

        private static void AddDetailsToEntity(BookDefault entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<BookDefault>();
            entity.Name = newEntity.Name;
            entity.Book = newEntity.Book;
            entity.Desk = newEntity.Desk;
            entity.GfProductMapping = newEntity.GfProductMapping;
            entity.Trader = newEntity.Trader;
            entity.DefaultType = newEntity.DefaultType;
            entity.PartyRole = newEntity.PartyRole;
        }

        private static void CreateSearchData(Search search, BookDefault entity1, BookDefault entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
