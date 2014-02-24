namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    public static class SettlementContactData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static SettlementContactData()
        {
            repository = ObjectScript.Repository;
        }

        public static SettlementContact CreateBasicEntity()
        {
            var entity = ObjectMother.Create<SettlementContact>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static SettlementContact CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<SettlementContact>();

            var endurMapping = new PartyAccountabilityMapping
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

        public static RWEST.Nexus.MDM.Contracts.SettlementContact CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.SettlementContact();
            AddDetailsToContract(contract);
            return contract;
        }

        public static SettlementContact CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new SettlementContact();
            entity.SourceParty = ObjectMother.Create<Party>();
            entity.TargetParty = ObjectMother.Create<Party>();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new PartyAccountabilityMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new PartyAccountabilityMapping
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

        public static void CreateSearch(Search search, SettlementContact entity1, SettlementContact entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.SettlementContact MakeChangeToContract(RWEST.Nexus.MDM.Contracts.SettlementContact currentContract)
        {
            var entity = ObjectMother.Create<SettlementContact>();
            
            currentContract.Details = new RWEST.Nexus.MDM.Contracts.SettlementContactDetails()
                {
                    Name = entity.Name,
                };

            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.SettlementContact contract)
        {
            var entity = ObjectMother.Create<SettlementContact>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new RWEST.Nexus.MDM.Contracts.SettlementContactDetails()
                {
                    Name = entity.Name,
                    CommodityInstrumentType = entity.CommodityInstrumentType.CreateNexusEntityId(()=> ""),
                    Location = entity.Location.CreateNexusEntityId(()=> ""),
                };

            contract.Details.SourceParty = entity.SourceParty.CreateNexusEntityId(() => "");
            contract.Details.TargetParty = entity.TargetParty.CreateNexusEntityId(() => "");
            contract.Details.SourcePerson = entity.SourcePerson.CreateNexusEntityId(() => "");
            contract.Details.TargetPerson = entity.SourcePerson.CreateNexusEntityId(() => "");


            // delete entity as we will attempt to post this exact contract and we may violate integrity constraints
            // the purpose of the persistence is to save the related object graph
            repository.Delete(entity);
            repository.Flush();
        }

        private static void AddDetailsToEntity(SettlementContact entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<SettlementContact>();
            entity.Name = newEntity.Name;
            entity.CommodityInstrumentType = newEntity.CommodityInstrumentType;
            entity.Location = newEntity.Location;
        }

        private static void CreateSearchData(Search search, SettlementContact entity1, SettlementContact entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.Name);
        }
    }
}

