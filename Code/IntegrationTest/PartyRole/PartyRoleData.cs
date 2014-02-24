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
    using PartyRole = EnergyTrading.MDM.PartyRole;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class PartyRoleData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static PartyRoleData()
        {
            repository = ObjectScript.Repository;
        }

        public static PartyRole CreateBasicEntity()
        {
            var entity = ObjectMother.Create<PartyRole>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static PartyRole CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<PartyRole>();

            var endurMapping = new PartyRoleMapping
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

        public static RWEST.Nexus.MDM.Contracts.PartyRole CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.PartyRole();
            AddDetailsToContract(contract);
            return contract;
        }

        public static PartyRole CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new PartyRole();
            entity.Party = ObjectMother.Create<MDM.Party>();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new PartyRoleMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new PartyRoleMapping
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

        public static void CreateSearch(Search search, PartyRole entity1, PartyRole entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.PartyRole MakeChangeToContract(RWEST.Nexus.MDM.Contracts.PartyRole currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.PartyRole contract)
        {
            var entity = ObjectMother.Create<PartyRole>();
            repository.Add(entity);
            repository.Flush();

            contract.PartyRoleType = entity.PartyRoleType;
            contract.Details = new PartyRoleDetails()
                {
                    Name = entity.LatestDetails.Name,
                };

            contract.Party = entity.Party.CreateNexusEntityId(() => "");
        }

        private static void AddDetailsToEntity(PartyRole entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<PartyRole>();
            entity.PartyRoleType = newEntity.PartyRoleType;
            entity.AddDetails(newEntity.LatestDetails);
        }

        private static void CreateSearchData(Search search, PartyRole entity1, PartyRole entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.LatestDetails.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.LatestDetails.Name);
        }
    }
}

