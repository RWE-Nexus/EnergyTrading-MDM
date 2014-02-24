namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    public static class BrokerData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static BrokerData()
        {
            repository = ObjectScript.Repository;
        }

        public static Broker CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Broker>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Broker CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Broker>();

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

        public static RWEST.Nexus.MDM.Contracts.Broker CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Broker();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Broker CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Broker();
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

        public static void CreateSearch(Search search, Broker entity1, Broker entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Broker MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Broker currentContract)
        {
            var entity = ObjectMother.Create<Broker>();
            repository.Add(entity);
            repository.Flush();

            currentContract.Details = new RWEST.Nexus.MDM.Contracts.BrokerDetails()
                {
                    Name = entity.LatestDetails.Name,
                    Phone = (entity.LatestDetails as BrokerDetails).Phone,
                    Fax = (entity.LatestDetails as BrokerDetails).Fax,
                    Rate = (entity.LatestDetails as BrokerDetails).Rate,
                };

            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Broker contract)
        {
            var entity = ObjectMother.Create<Broker>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new RWEST.Nexus.MDM.Contracts.BrokerDetails()
                {
                    Name = entity.LatestDetails.Name,
                    Phone = (entity.LatestDetails as BrokerDetails).Phone,
                    Fax = (entity.LatestDetails as BrokerDetails).Fax,
                    Rate = (entity.LatestDetails as BrokerDetails).Rate,
                };
            
            contract.Party = entity.Party.CreateNexusEntityId(() => "");
        }

        private static void AddDetailsToEntity(Broker entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<Broker>();

            entity.PartyRoleType = newEntity.PartyRoleType;
            entity.AddDetails(new BrokerDetails() { 
                Name = newEntity.Details[0].Name, 
                Phone = ((BrokerDetails)newEntity.Details[0]).Phone,
                Fax = ((BrokerDetails)newEntity.Details[0]).Fax,
                Rate = ((BrokerDetails)newEntity.Details[0]).Rate});
        }

        private static void CreateSearchData(Search search, Broker entity1, Broker entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.LatestDetails.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.LatestDetails.Name);
        }
    }
}
