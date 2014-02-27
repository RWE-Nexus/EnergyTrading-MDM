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

    using CommodityInstrumentType = EnergyTrading.MDM.CommodityInstrumentType;
    using DateRange = EnergyTrading.DateRange;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class CommodityInstrumentTypeData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static CommodityInstrumentTypeData()
        {
            repository = ObjectScript.Repository;
        }

        public static CommodityInstrumentType CreateBasicEntity()
        {
            var entity = ObjectMother.Create<CommodityInstrumentType>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static CommodityInstrumentType CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<CommodityInstrumentType>();

            var endurMapping = new CommodityInstrumentTypeMapping
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

        public static RWEST.Nexus.MDM.Contracts.CommodityInstrumentType CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.CommodityInstrumentType();
            AddDetailsToContract(contract);
            return contract;
        }

        public static CommodityInstrumentType CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new CommodityInstrumentType();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new CommodityInstrumentTypeMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new CommodityInstrumentTypeMapping
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

        public static void CreateSearch(Search search, CommodityInstrumentType entity1, CommodityInstrumentType entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.CommodityInstrumentType MakeChangeToContract(RWEST.Nexus.MDM.Contracts.CommodityInstrumentType currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.CommodityInstrumentType contract)
        {
            var entity = ObjectMother.Create<CommodityInstrumentType>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new CommodityInstrumentTypeDetails
            {
                Commodity = entity.Commodity.CreateNexusEntityId(() => entity.Commodity.Name),
                InstrumentType = entity.InstrumentType.CreateNexusEntityId(() => entity.InstrumentType.Name),
                InstrumentDelivery = entity.InstrumentDelivery,
            };

            // delete entity as we will attempt to post this exact contract and we may violate integrity constraints
            // the purpose of the persistence is to save the related object graph
            repository.Delete(entity);
            repository.Flush();
        }

        private static void AddDetailsToEntity(CommodityInstrumentType entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<CommodityInstrumentType>();
            entity.Commodity = newEntity.Commodity;
            entity.InstrumentType = newEntity.InstrumentType;
            entity.InstrumentDelivery = newEntity.InstrumentDelivery;
        }

        private static void CreateSearchData(Search search, CommodityInstrumentType entity1, CommodityInstrumentType entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
