using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;
    using DateRange = EnergyTrading.DateRange;
    using Portfolio = EnergyTrading.MDM.Portfolio;
    using Product = EnergyTrading.MDM.Product;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class PortfolioData
    {
        // <see cref="RWEST.Nexus.MDM.Contracts.PortfolioDetails">
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static PortfolioData()
        {
            repository = ObjectScript.Repository;
        }

        public static Portfolio CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Portfolio>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Portfolio CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Portfolio>();

            var endurMapping = new PortfolioMapping
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

        public static RWEST.Nexus.MDM.Contracts.Portfolio CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Portfolio();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Portfolio CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Portfolio();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new PortfolioMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new PortfolioMapping
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

        public static void CreateSearch(Search search, Portfolio entity1, Portfolio entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Portfolio MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Portfolio currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Portfolio contract)
        {
            var portfolio = ObjectMother.Create<Portfolio>();
            repository.Add(portfolio);
            repository.Flush();

            contract.Details = new PortfolioDetails()
                {
                    Name = Guid.NewGuid().ToString(),
                    PortfolioType = portfolio.PortfolioType,
                    BusinessUnit = portfolio.BusinessUnit.CreateNexusEntityId(() => portfolio.BusinessUnit.LatestDetails.Name)
                };
        }

        private static void AddDetailsToEntity(Portfolio entity, DateTime startDate, DateTime endDate)
        {
            var portfolio = ObjectMother.Create<Portfolio>();
            repository.Add(portfolio);
            repository.Flush();

            entity.PortfolioType = portfolio.PortfolioType;
            entity.Name = Guid.NewGuid().ToString();
            entity.BusinessUnit = portfolio.BusinessUnit;
            
        }

        private static void CreateSearchData(Search search, Portfolio entity1, Portfolio entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
