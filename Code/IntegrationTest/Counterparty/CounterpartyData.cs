namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    public static class CounterpartyData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static CounterpartyData()
        {
            repository = ObjectScript.Repository;
        }

        public static Counterparty CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Counterparty>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Counterparty CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Counterparty>();

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

        public static RWEST.Nexus.MDM.Contracts.Counterparty CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Counterparty();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Counterparty CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Counterparty();
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

        public static void CreateSearch(Search search, Counterparty entity1, Counterparty entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Counterparty MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Counterparty currentContract)
        {
            var entity = ObjectMother.Create<Counterparty>();
            repository.Add(entity);
            repository.Flush();

            currentContract.Details = new RWEST.Nexus.MDM.Contracts.CounterpartyDetails()
                {
                    Name = entity.LatestDetails.Name,
                    Phone = (entity.LatestDetails as CounterpartyDetails).Phone,
                    Fax = (entity.LatestDetails as CounterpartyDetails).Fax,
                    ShortName = (entity.LatestDetails as CounterpartyDetails).ShortName,
                };
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Counterparty contract)
        {
            var entity = ObjectMother.Create<Counterparty>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new RWEST.Nexus.MDM.Contracts.CounterpartyDetails()
                {
                    Name = entity.LatestDetails.Name,
                    Phone = (entity.LatestDetails as CounterpartyDetails).Phone,
                    Fax = (entity.LatestDetails as CounterpartyDetails).Fax,
                    ShortName = (entity.LatestDetails as CounterpartyDetails).ShortName,
                };
            contract.Party = entity.Party.CreateNexusEntityId(() => "");
        }

        private static void AddDetailsToEntity(Counterparty entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<Counterparty>();
            entity.PartyRoleType = newEntity.PartyRoleType;
            entity.AddDetails(new CounterpartyDetails() { 
                Name = newEntity.Details[0].Name, 
                Phone = ((CounterpartyDetails)newEntity.Details[0]).Phone,
                Fax = ((CounterpartyDetails)newEntity.Details[0]).Fax,
                ShortName = ((CounterpartyDetails)newEntity.Details[0]).ShortName, 
            });
        }

        private static void CreateSearchData(Search search, Counterparty entity1, Counterparty entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.LatestDetails.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.LatestDetails.Name);
        }
    }
}

