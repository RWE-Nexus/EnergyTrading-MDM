namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Data.EntityFramework;
	using EnergyTrading.Contracts.Search;

    using DateRange = EnergyTrading.DateRange;

    public partial class FeeTypeData
    {
        private readonly DbSetRepository repository;

        private DateTime baseDate;

        public FeeTypeData(DbSetRepository repository)
        {
            this.repository = repository;
        }

        public MDM.FeeType CreateEntityWithTwoDetailsAndTwoMappings()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Gastar").First();
            var trayport = repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new MDM.FeeType();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            this.AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            this.AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new FeeTypeMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue)
                };

            var endurMapping = new FeeTypeMapping()
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

        public MDM.FeeType CreateBasicEntity()
        {
            var entity = ObjectMother.Create<MDM.FeeType>();
            this.repository.Add(entity);
            this.repository.Flush();
            return entity;
        }

        public MDM.FeeType CreateBasicEntityWithOneMapping()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Gastar").First();

            var entity = ObjectMother.Create<MDM.FeeType>();

            var endurMapping = new FeeTypeMapping()
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

        public RWEST.Nexus.MDM.Contracts.FeeType MakeChangeToContract(RWEST.Nexus.MDM.Contracts.FeeType currentContract)
        {
            this.AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        public RWEST.Nexus.MDM.Contracts.FeeType CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.FeeType();
            this.AddDetailsToContract(contract);
            return contract;
        }

        public void CreateSearch(Search search, MDM.FeeType entity1, MDM.FeeType entity2)
        {
            this.CreateSearchData(search, entity1, entity2) ;
        }

        partial void CreateSearchData(Search search, MDM.FeeType entity1, MDM.FeeType entity2);

        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.FeeType contract);

        partial void AddDetailsToEntity(MDM.FeeType entity, DateTime startDate, DateTime endDate);

    }
}
