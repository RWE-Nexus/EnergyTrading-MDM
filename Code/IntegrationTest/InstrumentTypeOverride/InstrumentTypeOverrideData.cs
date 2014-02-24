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

    public static class InstrumentTypeOverrideData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static InstrumentTypeOverrideData()
        {
            repository = ObjectScript.Repository;
        }

        public static InstrumentTypeOverride CreateBasicEntity()
        {
            var entity = ObjectMother.Create<InstrumentTypeOverride>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static InstrumentTypeOverride CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<InstrumentTypeOverride>();

            var endurMapping = new InstrumentTypeOverrideMapping
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

        public static RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride();
            AddDetailsToContract(contract);
            return contract;
        }

        public static InstrumentTypeOverride CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new InstrumentTypeOverride();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new InstrumentTypeOverrideMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new InstrumentTypeOverrideMapping
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

        public static void CreateSearch(Search search, InstrumentTypeOverride entity1, InstrumentTypeOverride entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride MakeChangeToContract(RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride contract)
        {
            var entity = ObjectMother.Create<InstrumentTypeOverride>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new InstrumentTypeOverrideDetails()
            {
                Name = Guid.NewGuid().ToString(),
                ProductType = entity.ProductType.CreateNexusEntityId(() => entity.ProductType.Name),
                Broker = entity.Broker.CreateNexusEntityId(() => entity.Broker.LatestDetails.Name),
                CommodityInstrumentType =
                       entity.CommodityInstrumentType.CreateNexusEntityId(
                           () =>
                           string.Format(
                               "{0}|{1}|{2}",
                               entity.CommodityInstrumentType.Commodity == null ? string.Empty : entity.CommodityInstrumentType.Commodity.Name,
                               entity.CommodityInstrumentType.InstrumentType == null ? string.Empty : entity.CommodityInstrumentType.InstrumentType.Name,
                               entity.CommodityInstrumentType.InstrumentDelivery)),
                InstrumentSubType= "financial",
                ProductTenorType = entity.ProductTenorType.CreateNexusEntityId(() =>
                    string.Format("{0}|{1}", entity.ProductTenorType.Product.Name, entity.ProductTenorType.TenorType.Name)),
            };
        }

        private static void AddDetailsToEntity(InstrumentTypeOverride entity, DateTime startDate, DateTime endDate)
        {
			var newEntity = ObjectMother.Create<InstrumentTypeOverride>();
            entity.Name = Guid.NewGuid().ToString();
            entity.ProductType = newEntity.ProductType;
            entity.Broker = newEntity.Broker;
            entity.CommodityInstrumentType = newEntity.CommodityInstrumentType;
            entity.InstrumentSubType = "fixed";
            entity.ProductTenorType = newEntity.ProductTenorType;
        }

        private static void CreateSearchData(Search search, InstrumentTypeOverride entity1, InstrumentTypeOverride entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
