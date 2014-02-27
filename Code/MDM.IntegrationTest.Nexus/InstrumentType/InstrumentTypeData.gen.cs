namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Data.EntityFramework;
	using EnergyTrading.Contracts.Search;

    using DateRange = EnergyTrading.DateRange;

    public partial class InstrumentTypeData
    {
        private readonly DbSetRepository repository;

        private DateTime baseDate;

        public InstrumentTypeData(DbSetRepository repository)
        {
            this.repository = repository;
        }

        public MDM.InstrumentType CreateEntityWithTwoDetailsAndTwoMappings()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            var trayport = repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new MDM.InstrumentType();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            this.AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            this.AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new InstrumentTypeMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue)
                };

            var endurMapping = new InstrumentTypeMapping()
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

        public MDM.InstrumentType CreateBasicEntity()
        {
            var entity = ObjectMother.Create<InstrumentType>();
            this.repository.Add(entity);
            this.repository.Flush();
            return entity;
        }

        public MDM.InstrumentType CreateBasicEntityWithOneMapping()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<InstrumentType>();

            var endurMapping = new InstrumentTypeMapping()
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

        public RWEST.Nexus.MDM.Contracts.InstrumentType MakeChangeToContract(RWEST.Nexus.MDM.Contracts.InstrumentType currentContract)
        {
            this.AddDetailsToContract(currentContract);
            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        public RWEST.Nexus.MDM.Contracts.InstrumentType CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.InstrumentType();
            this.AddDetailsToContract(contract);
            return contract;
        }

        public void CreateSearch(Search search, InstrumentType entity1, InstrumentType entity2)
        {
            this.CreateSearchData(search, entity1, entity2) ;
        }

        partial void CreateSearchData(Search search, InstrumentType entity1, InstrumentType entity2);

        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.InstrumentType contract);

        partial void AddDetailsToEntity(MDM.InstrumentType entity, DateTime startDate, DateTime endDate);

    }
}
