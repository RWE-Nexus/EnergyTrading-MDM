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
    using PortfolioHierarchy = EnergyTrading.MDM.PortfolioHierarchy;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class PortfolioHierarchyData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static PortfolioHierarchyData()
        {
            repository = ObjectScript.Repository;
        }

        public static PortfolioHierarchy CreateBasicEntity()
        {
            var entity = ObjectMother.Create<PortfolioHierarchy>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static PortfolioHierarchy CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<PortfolioHierarchy>();

            var endurMapping = new PortfolioHierarchyMapping
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

        public static RWEST.Nexus.MDM.Contracts.PortfolioHierarchy CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.PortfolioHierarchy();
            AddDetailsToContract(contract);
            return contract;
        }

        public static PortfolioHierarchy CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new PortfolioHierarchy();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new PortfolioHierarchyMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new PortfolioHierarchyMapping
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

        public static void CreateSearch(Search search, PortfolioHierarchy entity1, PortfolioHierarchy entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.PortfolioHierarchy MakeChangeToContract(RWEST.Nexus.MDM.Contracts.PortfolioHierarchy currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.PortfolioHierarchy contract)
        {
            var portfolioHierarchy = ObjectMother.Create<PortfolioHierarchy>();
            repository.Add(portfolioHierarchy);
            repository.Flush();

            contract.Details = new PortfolioHierarchyDetails()
                {
                    ChildPortfolio = portfolioHierarchy.ChildPortfolio.CreateNexusEntityId(() => portfolioHierarchy.ChildPortfolio.Name),
                    ParentPortfolio = portfolioHierarchy.ParentPortfolio.CreateNexusEntityId(() => portfolioHierarchy.ParentPortfolio.Name),
                    Hierarchy = portfolioHierarchy.Hierarachy.CreateNexusEntityId(() => portfolioHierarchy.Hierarachy.Name),
                };
        }

        private static void AddDetailsToEntity(PortfolioHierarchy entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<PortfolioHierarchy>();
            entity.ParentPortfolio = newEntity.ParentPortfolio;
            entity.ChildPortfolio = newEntity.ChildPortfolio;
            entity.Hierarachy = newEntity.Hierarachy;
        }

        private static void CreateSearchData(Search search, PortfolioHierarchy entity1, PortfolioHierarchy entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
