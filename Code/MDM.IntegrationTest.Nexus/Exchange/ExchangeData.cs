namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    public static class ExchangeData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static ExchangeData()
        {
            repository = ObjectScript.Repository;
        }

        public static Exchange CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Exchange>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Exchange CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Exchange>();

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

        public static RWEST.Nexus.MDM.Contracts.Exchange CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Exchange();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Exchange CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Exchange();
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

        public static void CreateSearch(Search search, Exchange entity1, Exchange entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Exchange MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Exchange currentContract)
        {
            var entity = ObjectMother.Create<Exchange>();
            repository.Add(entity);
            repository.Flush();

            currentContract.Details = new RWEST.Nexus.MDM.Contracts.ExchangeDetails()
                {
                    Name = entity.LatestDetails.Name,
                    Phone = (entity.LatestDetails as ExchangeDetails).Phone,
                    Fax = (entity.LatestDetails as ExchangeDetails).Fax,
                };

            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Exchange contract)
        {
            var entity = ObjectMother.Create<Exchange>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new RWEST.Nexus.MDM.Contracts.ExchangeDetails()
                {
                    Name = entity.Details[0].Name,
                    Phone = (entity.Details[0] as ExchangeDetails).Phone,
                    Fax = (entity.Details[0] as ExchangeDetails).Fax,
                };

            contract.Party = entity.Party.CreateNexusEntityId(() => "");
        }

        private static void AddDetailsToEntity(Exchange entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<Exchange>();
            entity.PartyRoleType = newEntity.PartyRoleType;
            entity.AddDetails(new ExchangeDetails() { 
                Name = newEntity.Details[0].Name, 
                Phone = ((ExchangeDetails)newEntity.Details[0]).Phone,
                Fax = ((ExchangeDetails)newEntity.Details[0]).Fax });
        }

        private static void CreateSearchData(Search search, Exchange entity1, Exchange entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.LatestDetails.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.LatestDetails.Name);
        }
    }
}
