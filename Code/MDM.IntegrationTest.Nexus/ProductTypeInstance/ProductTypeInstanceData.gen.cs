namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Data.EntityFramework;
	using EnergyTrading.Contracts.Search;

    using DateRange = EnergyTrading.DateRange;

    public partial class ProductTypeInstanceData
    {
        private readonly DbSetRepository repository;

        private DateTime baseDate;

        public ProductTypeInstanceData(DbSetRepository repository)
        {
            this.repository = repository;
        }

        public MDM.ProductTypeInstance CreateEntityWithTwoDetailsAndTwoMappings()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            var trayport = repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new MDM.ProductTypeInstance();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            this.AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            this.AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new ProductTypeInstanceMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue)
                };

            var endurMapping = new ProductTypeInstanceMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = endur,
                    IsDefault = true,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue)
                };

            entity.ProcessMapping(trayportMapping);
            entity.ProcessMapping(endurMapping);

            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public MDM.ProductTypeInstance CreateBasicEntity()
        {
            var entity = ObjectMother.Create<ProductTypeInstance>();
            this.repository.Add(entity);
            this.repository.Flush();
            return entity;
        }

        public MDM.ProductTypeInstance CreateBasicEntityWithOneMapping()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<ProductTypeInstance>();

            var endurMapping = new ProductTypeInstanceMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = endur,
                    IsDefault = true,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue.Subtract(new TimeSpan(72, 0, 0)))
                };

            entity.ProcessMapping(endurMapping);
            this.repository.Add(entity);
            this.repository.Flush();

            return entity;
        }

        public RWEST.Nexus.MDM.Contracts.ProductTypeInstance MakeChangeToContract(RWEST.Nexus.MDM.Contracts.ProductTypeInstance currentContract)
        {
            this.AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        public RWEST.Nexus.MDM.Contracts.ProductTypeInstance CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.ProductTypeInstance();
            this.AddDetailsToContract(contract);
            return contract;
        }

        public void CreateSearch(Search search, ProductTypeInstance entity1, ProductTypeInstance entity2)
        {
            this.CreateSearchData(search, entity1, entity2) ;
        }

        partial void CreateSearchData(Search search, ProductTypeInstance entity1, ProductTypeInstance entity2);

        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ProductTypeInstance contract);

        partial void AddDetailsToEntity(MDM.ProductTypeInstance entity, DateTime startDate, DateTime endDate);

    }
}
