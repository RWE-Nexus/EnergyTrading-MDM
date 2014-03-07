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
    using PartyOverride = EnergyTrading.MDM.PartyOverride;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class PartyOverrideData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static PartyOverrideData()
        {
            repository = ObjectScript.Repository;
        }

        public static PartyOverride CreateBasicEntity()
        {
            var entity = ObjectMother.Create<PartyOverride>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static PartyOverride CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<PartyOverride>();

            var endurMapping = new PartyOverrideMapping
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

        public static RWEST.Nexus.MDM.Contracts.PartyOverride CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.PartyOverride();
            AddDetailsToContract(contract);
            return contract;
        }

        public static PartyOverride CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new PartyOverride();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new PartyOverrideMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new PartyOverrideMapping
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

        public static void CreateSearch(Search search, PartyOverride entity1, PartyOverride entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.PartyOverride MakeChangeToContract(RWEST.Nexus.MDM.Contracts.PartyOverride currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.PartyOverride contract)
        {
            var entity = ObjectMother.Create<PartyOverride>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new PartyOverrideDetails()
            {
                Broker = entity.Broker.CreateNexusEntityId(() => entity.Broker.LatestDetails.Name),
                CommodityInstrumentType =
                       entity.CommodityInstrumentType.CreateNexusEntityId(
                           () =>
                           string.Format(
                               "{0}|{1}|{2}",
                               entity.CommodityInstrumentType.Commodity == null ? string.Empty : entity.CommodityInstrumentType.Commodity.Name,
                               entity.CommodityInstrumentType.InstrumentType == null ? string.Empty : entity.CommodityInstrumentType.InstrumentType.Name,
                               entity.CommodityInstrumentType.InstrumentDelivery)),
                
                MappingValue = "mappingValue",
                Party = entity.Party.CreateNexusEntityId(() => entity.Party.LatestDetails.Name),
            };
        }

        private static void AddDetailsToEntity(PartyOverride entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<PartyOverride>();
            entity.Broker = newEntity.Broker;
            entity.CommodityInstrumentType = newEntity.CommodityInstrumentType;
            entity.MappingValue = "mappingValue";
            entity.Party = newEntity.Party;
        }

        private static void CreateSearchData(Search search, PartyOverride entity1, PartyOverride entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
