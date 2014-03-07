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

    using Broker = EnergyTrading.MDM.Broker;
    using BrokerRate = EnergyTrading.MDM.BrokerRate;
    using CommodityInstrumentType = EnergyTrading.MDM.CommodityInstrumentType;
    using DateRange = EnergyTrading.DateRange;
    using Party = EnergyTrading.MDM.Party;
    using PartyOverride = EnergyTrading.MDM.PartyOverride;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class BrokerRateData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static BrokerRateData()
        {
            repository = ObjectScript.Repository;
        }

        public static BrokerRate CreateBasicEntity()
        {
            var entity = ObjectMother.Create<BrokerRate>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static BrokerRate CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<BrokerRate>();

            var endurMapping = new BrokerRateMapping
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

        public static RWEST.Nexus.MDM.Contracts.BrokerRate CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.BrokerRate();
            AddDetailsToContract(contract);
            return contract;
        }

        public static BrokerRate CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new BrokerRate();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new BrokerRateMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new BrokerRateMapping
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

        public static void CreateSearch(Search search, BrokerRate entity1, BrokerRate entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.BrokerRate MakeChangeToContract(RWEST.Nexus.MDM.Contracts.BrokerRate currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.BrokerRate contract)
        {
            var entity = ObjectMother.Create<BrokerRate>();
            repository.Add(entity);
            repository.Flush();
          
            contract.Details.Broker = entity.LatestDetails.Broker.CreateNexusEntityId(() => entity.LatestDetails.Broker.LatestDetails.Name);
            contract.Details.CommodityInstrumentType = entity.LatestDetails.CommodityInstrumentType.CreateNexusEntityId(() =>string.Format("{0}|{1}|{2}",entity.LatestDetails.CommodityInstrumentType.Commodity == null ? string.Empty : entity.LatestDetails.CommodityInstrumentType.Commodity.Name, entity.LatestDetails.CommodityInstrumentType.InstrumentType == null ? string.Empty : entity.LatestDetails.CommodityInstrumentType.InstrumentType.Name,entity.LatestDetails.CommodityInstrumentType.InstrumentDelivery));
            contract.Details.Desk = entity.LatestDetails.Desk.CreateNexusEntityId(() => entity.LatestDetails.Desk.LatestDetails.Name);
            contract.Details.Location = entity.LatestDetails.Location.CreateNexusEntityId(() => entity.LatestDetails.Location.Name);
            contract.Details.ProductType = entity.LatestDetails.ProductType.CreateNexusEntityId(() => entity.LatestDetails.ProductType.Name);
            contract.Details.PartyAction = PartyAction.Aggressor;
            contract.Details.Rate = 4.5m;
            contract.Details.RateType = "per unit";
            contract.Details.Currency = "GBP";

        }

        private static void AddDetailsToEntity(BrokerRate entity, DateTime startDate, DateTime endDate)
        {
           var newEntity = ObjectMother.Create<EnergyTrading.MDM.BrokerRateDetails>();
           entity.AddDetails(newEntity);
        }

        private static void CreateSearchData(Search search, BrokerRate entity1, BrokerRate entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("BrokerRate.Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("BrokerRate.Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
