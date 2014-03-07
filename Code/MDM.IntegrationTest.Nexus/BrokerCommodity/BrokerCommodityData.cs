using RWEST.Nexus.MDM.Contracts;
using EnergyTrading.MDM.Extensions;
using EnergyTrading.Search;

namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;

    public static class BrokerCommodityData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static BrokerCommodityData()
        {
            repository = ObjectScript.Repository;
        }

        public static BrokerCommodity CreateBasicEntity()
        {
            var entity = ObjectMother.Create<BrokerCommodity>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static BrokerCommodity CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<BrokerCommodity>();

            var endurMapping = new BrokerCommodityMapping
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

        public static RWEST.Nexus.MDM.Contracts.BrokerCommodity CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.BrokerCommodity();
            AddDetailsToContract(contract);
            return contract;
        }

        public static BrokerCommodity CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new BrokerCommodity();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new BrokerCommodityMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new BrokerCommodityMapping
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

        public static void CreateSearch(Search search, BrokerCommodity entity1, BrokerCommodity entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.BrokerCommodity MakeChangeToContract(RWEST.Nexus.MDM.Contracts.BrokerCommodity currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.BrokerCommodity contract)
        {
            var entity = ObjectMother.Create<BrokerCommodity>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new BrokerCommodityDetails()
            {
                Name = entity.Name,
                Broker = entity.Broker.CreateNexusEntityId(() => entity.Broker.LatestDetails.Name),
                Commodity = entity.Commodity.CreateNexusEntityId(() => entity.Commodity.Name),

                
            };
        }

        private static void AddDetailsToEntity(BrokerCommodity entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<BrokerCommodity>();
            entity.Broker = newEntity.Broker;
            entity.Commodity = newEntity.Commodity;
            entity.Name = newEntity.Name;
        }

        private static void CreateSearchData(Search search, BrokerCommodity entity1, BrokerCommodity entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
