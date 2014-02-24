namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.Search;
    using EnergyTrading.MDM.Extensions;

    public static class LegalEntityData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static LegalEntityData()
        {
            repository = ObjectScript.Repository;
        }

        public static LegalEntity CreateBasicEntity()
        {
            var entity = ObjectMother.Create<LegalEntity>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static LegalEntity CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<LegalEntity>();

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

        public static RWEST.Nexus.MDM.Contracts.LegalEntity CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.LegalEntity();
            AddDetailsToContract(contract);
            return contract;
        }

        public static LegalEntity CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new LegalEntity();
            entity.Party = ObjectMother.Create<Party>();
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

        public static void CreateSearch(Search search, LegalEntity entity1, LegalEntity entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.LegalEntity MakeChangeToContract(RWEST.Nexus.MDM.Contracts.LegalEntity currentContract)
        {
            var entity = ObjectMother.Create<LegalEntity>();
            repository.Add(entity);
            repository.Flush();

            currentContract.Details = new RWEST.Nexus.MDM.Contracts.LegalEntityDetails()
            {
                Name = entity.LatestDetails.Name,
                Phone = (entity.LatestDetails as LegalEntityDetails).Phone,
                Fax = (entity.LatestDetails as LegalEntityDetails).Fax,
            };

            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.LegalEntity contract)
        {
            var entity = ObjectMother.Create<LegalEntity>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new RWEST.Nexus.MDM.Contracts.LegalEntityDetails()
            {
                Name = entity.LatestDetails.Name,
                Phone = (entity.LatestDetails as LegalEntityDetails).Phone,
            };
            contract.Party = entity.Party.CreateNexusEntityId(() => "");
        }

        private static void AddDetailsToEntity(LegalEntity entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<LegalEntity>();

            entity.PartyRoleType = newEntity.PartyRoleType;
            entity.AddDetails(new LegalEntityDetails
            {
                Name = newEntity.Details[0].Name,
            });
        }

        private static void CreateSearchData(Search search, LegalEntity entity1, LegalEntity entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.LatestDetails.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.LatestDetails.Name);
        }
    }
}
