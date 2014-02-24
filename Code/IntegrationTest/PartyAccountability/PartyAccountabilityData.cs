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
    using PartyAccountability = EnergyTrading.MDM.PartyAccountability;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class PartyAccountabilityData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static PartyAccountabilityData()
        {
            repository = ObjectScript.Repository;
        }

        public static PartyAccountability CreateBasicEntity()
        {
            var entity = ObjectMother.Create<PartyAccountability>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static PartyAccountability CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<PartyAccountability>();

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

        public static RWEST.Nexus.MDM.Contracts.PartyAccountability CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.PartyAccountability();
            AddDetailsToContract(contract);
            return contract;
        }

        public static PartyAccountability CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new PartyAccountability();
            entity.TargetParty = ObjectMother.Create<MDM.Party>();
            entity.SourceParty = ObjectMother.Create<MDM.Party>();
            entity.PartyAccountabilityType = "PartyAccountability";
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

        public static void CreateSearch(Search search, PartyAccountability entity1, PartyAccountability entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.PartyAccountability MakeChangeToContract(RWEST.Nexus.MDM.Contracts.PartyAccountability currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.PartyAccountability contract)
        {
            var entity = ObjectMother.Create<PartyAccountability>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new PartyAccountabilityDetails()
                {
                    Name = entity.Name,
                };

            contract.Details.SourceParty = entity.SourceParty.CreateNexusEntityId(() => "");
            contract.Details.TargetParty = entity.TargetParty.CreateNexusEntityId(() => "");
            contract.Details.PartyAccountabilityType = "PartyAccountability";

            // delete entity as we will attempt to post this exact contract and we may violate integrity constraints
            // the purpose of the persistence is to save the related object graph
            repository.Delete(entity);
            repository.Flush();
        }

        private static void AddDetailsToEntity(PartyAccountability entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<PartyAccountability>();
            entity.AddDetails(newEntity);
        }

        private static void CreateSearchData(Search search, PartyAccountability entity1, PartyAccountability entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.Name);
        }
    }
}


