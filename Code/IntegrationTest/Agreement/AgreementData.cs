using RWEST.Nexus.MDM.Contracts;
using EnergyTrading.Search;

namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;

    public static class AgreementData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static AgreementData()
        {
            repository = ObjectScript.Repository;
        }

        public static Agreement CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Agreement>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Agreement CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<Agreement>();

            var endurMapping = new AgreementMapping
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

        public static RWEST.Nexus.MDM.Contracts.Agreement CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Agreement();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Agreement CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Agreement();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new AgreementMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new AgreementMapping
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

        public static void CreateSearch(Search search, Agreement entity1, Agreement entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Agreement MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Agreement currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Agreement contract)
        {
            // TODO_IntegrationTestGeneration_Agreement - Add details to a contract

            var entity = ObjectMother.Create<Agreement>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new AgreementDetails()
                {
                    Name = entity.Name,
                    PaymentTerms = entity.PaymentTerms
                };
        }

        private static void AddDetailsToEntity(Agreement entity, DateTime startDate, DateTime endDate)
        {
            // TODO_IntegrationTestGeneration_Agreement - Add details to an entity

            var newEntity = ObjectMother.Create<Agreement>();
            entity.Name = newEntity.Name;
            entity.PaymentTerms = newEntity.PaymentTerms;
        }

        private static void CreateSearchData(Search search, Agreement entity1, Agreement entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
