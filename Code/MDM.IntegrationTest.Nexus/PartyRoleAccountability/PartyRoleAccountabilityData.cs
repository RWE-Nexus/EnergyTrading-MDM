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
    using PartyRoleAccountability = EnergyTrading.MDM.PartyRoleAccountability;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class PartyRoleAccountabilityData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static PartyRoleAccountabilityData()
        {
            repository = ObjectScript.Repository;
        }

        public static PartyRoleAccountability CreateBasicEntity()
        {
            var entity = ObjectMother.Create<PartyRoleAccountability>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static PartyRoleAccountability CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<PartyRoleAccountability>();

            var endurMapping = new PartyRoleAccountabilityMapping
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

        public static RWEST.Nexus.MDM.Contracts.PartyRoleAccountability CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.PartyRoleAccountability();
            AddDetailsToContract(contract);
            return contract;
        }

        public static PartyRoleAccountability CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new PartyRoleAccountability();
            entity.TargetPartyRole = ObjectMother.Create<MDM.PartyRole>();
            entity.SourcePartyRole = ObjectMother.Create<MDM.PartyRole>();
            entity.PartyRoleAccountabilityType = "PartyRoleAccountability";
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new PartyRoleAccountabilityMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new PartyRoleAccountabilityMapping
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

        public static void CreateSearch(Search search, PartyRoleAccountability entity1, PartyRoleAccountability entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.PartyRoleAccountability MakeChangeToContract(RWEST.Nexus.MDM.Contracts.PartyRoleAccountability currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.PartyRoleAccountability contract)
        {
            var entity = ObjectMother.Create<PartyRoleAccountability>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new PartyRoleAccountabilityDetails()
                {
                    Name = entity.Name,
                };

            contract.Details.SourcePartyRole = entity.SourcePartyRole.CreateNexusEntityId(() => "");
            contract.Details.TargetPartyRole = entity.TargetPartyRole.CreateNexusEntityId(() => "");
            contract.Details.PartyRoleAccountabilityType = "PartyRoleAccountability";

            // delete entity as we will attempt to post this exact contract and we may violate integrity constraints
            // the purpose of the persistence is to save the related object graph
            repository.Delete(entity);
            repository.Flush();
        }

        private static void AddDetailsToEntity(PartyRoleAccountability entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<PartyRoleAccountability>();
            entity.AddDetails(newEntity);
        }

        private static void CreateSearchData(Search search, PartyRoleAccountability entity1, PartyRoleAccountability entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.Name);
        }
    }
}


