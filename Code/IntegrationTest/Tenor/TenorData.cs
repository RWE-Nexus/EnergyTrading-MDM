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
    using SourceSystem = EnergyTrading.MDM.SourceSystem;
    using Tenor = EnergyTrading.MDM.Tenor;

    public static class TenorData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static TenorData()
        {
            repository = ObjectScript.Repository;
        }

        public static Tenor CreateBasicEntity()
        {
            var entity = ObjectMother.Create<Tenor>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static Tenor CreateBasicEntityWithOneMapping()
        {
            var endur = repository.Queryable<SourceSystem>().First(system => system.Name == "Endur");

            var entity = ObjectMother.Create<Tenor>();

            var endurMapping = new TenorMapping
                {
                    Tenor = entity,
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

        public static RWEST.Nexus.MDM.Contracts.Tenor CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.Tenor();
            AddDetailsToContract(contract);
            return contract;
        }

        public static Tenor CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new Tenor();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new TenorMapping
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var endurMapping = new TenorMapping
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

        public static void CreateSearch(Search search, Tenor entity1, Tenor entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.Tenor MakeChangeToContract(RWEST.Nexus.MDM.Contracts.Tenor currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Tenor contract)
        {
            var entity = ObjectMother.Create<Tenor>();
            repository.Add(entity.TenorType);
            repository.Flush();

            contract.Details = new TenorDetails
            {
                Name = entity.Name,
                ShortName = entity.ShortName,
                Delivery = new RWEST.Nexus.MDM.Contracts.DateRange { StartDate = entity.Delivery.Start, EndDate = entity.Delivery.Finish },
                DeliveryPeriod = entity.DeliveryPeriod,
                TenorType = entity.TenorType.CreateNexusEntityId(() => entity.TenorType.Name),
                Traded = new RWEST.Nexus.MDM.Contracts.DateRange { StartDate = entity.Traded.Start, EndDate = entity.Traded.Finish },
                IsRelative = entity.IsRelative,
                DeliveryRangeType = entity.DeliveryRangeType,
            };
        }

        private static void AddDetailsToEntity(Tenor entity, DateTime startDate, DateTime endDate)
        {
            var tenor = ObjectMother.Create<Tenor>();

            entity.Name = tenor.Name;
            entity.ShortName = tenor.ShortName;
            entity.TenorType = tenor.TenorType;
            entity.IsRelative = tenor.IsRelative;
            entity.DeliveryRangeType = tenor.DeliveryRangeType;
            entity.DeliveryPeriod = tenor.DeliveryPeriod;
            entity.Delivery = tenor.Delivery;
            entity.Traded = new DateRange(startDate, endDate);
        }

        private static void CreateSearchData(Search search, Tenor entity1, Tenor entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
