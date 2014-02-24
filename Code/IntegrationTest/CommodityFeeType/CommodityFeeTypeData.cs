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

    using CommodityFeeType = EnergyTrading.MDM.CommodityFeeType;
    using DateRange = EnergyTrading.DateRange;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    public static class CommodityFeeTypeData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static CommodityFeeTypeData()
        {
            repository = ObjectScript.Repository;
        }

        public static CommodityFeeType CreateBasicEntity()
        {
            var entity = ObjectMother.Create<CommodityFeeType>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static CommodityFeeType CreateBasicEntityWithOneMapping()
        {
            SourceSystem gastar = repository.Queryable<SourceSystem>().Where(system => system.Name == "Gastar").First();

            var entity = ObjectMother.Create<CommodityFeeType>();

            var gastarMapping = new CommodityFeeTypeMapping
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = gastar, 
                    IsDefault = true, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue.Subtract(new TimeSpan(72, 0, 0)))
                };

            entity.ProcessMapping(gastarMapping);
            repository.Add(entity);
            repository.Flush();

            return entity;
        }

        public static RWEST.Nexus.MDM.Contracts.CommodityFeeType CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.CommodityFeeType();
            AddDetailsToContract(contract);
            return contract;
        }

        public static CommodityFeeType CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem gastar = repository.Queryable<SourceSystem>().Where(system => system.Name == "Gastar").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new CommodityFeeType();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new CommodityFeeTypeMapping
                {
                    MappingValue = Guid.NewGuid().ToString(), 
                    System = trayport, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            var gastarMapping = new CommodityFeeTypeMapping
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = gastar, 
                    IsDefault = true, 
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            entity.ProcessMapping(trayportMapping);
            entity.ProcessMapping(gastarMapping);

            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static void CreateSearch(Search search, CommodityFeeType entity1, CommodityFeeType entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.CommodityFeeType MakeChangeToContract(RWEST.Nexus.MDM.Contracts.CommodityFeeType currentContract)
        {
            AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.CommodityFeeType contract)
        {
            var entity = ObjectMother.Create<CommodityFeeType>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new CommodityFeeTypeDetails
            {
                Commodity = entity.Commodity.CreateNexusEntityId(() => entity.Commodity.Name),
                FeeType  = entity.FeeType.CreateNexusEntityId(() => entity.FeeType.Name),
                
            };

            // delete entity as we will attempt to post this exact contract and we may violate integrity constraints
            // the purpose of the persistence is to save the related object graph
            repository.Delete(entity);
            repository.Flush();
        }

        private static void AddDetailsToEntity(CommodityFeeType entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<CommodityFeeType>();
            entity.Commodity = newEntity.Commodity;
            entity.FeeType = newEntity.FeeType;
            
        }

        private static void CreateSearchData(Search search, CommodityFeeType entity1, CommodityFeeType entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
