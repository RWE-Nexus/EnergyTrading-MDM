namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Data.EntityFramework;
	using EnergyTrading.Contracts.Search;

    using DateRange = EnergyTrading.DateRange;

    public partial class SourceSystemData
    {
        private readonly DbSetRepository repository;

        private DateTime baseDate;

        public SourceSystemData(DbSetRepository repository)
        {
            this.repository = repository;
        }

        public MDM.SourceSystem CreateEntityWithTwoDetailsAndTwoMappings()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            var trayport = repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new MDM.SourceSystem();
            baseDate = DateTime.Today.Subtract(new TimeSpan(72, 0, 0));
            SystemTime.UtcNow = () => new DateTime(DateTime.Today.Subtract(new TimeSpan(73, 0, 0)).Ticks);

            this.AddDetailsToEntity(entity, DateTime.MinValue, baseDate);
            this.AddDetailsToEntity(entity, baseDate, DateTime.MaxValue);

            SystemTime.UtcNow = () => DateTime.Now;

            var trayportMapping = new SourceSystemMapping()
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue,  DateTime.MaxValue)
                };

            var endurMapping = new SourceSystemMapping()
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

        public MDM.SourceSystem CreateBasicEntity()
        {
            var entity = ObjectMother.Create<SourceSystem>();
            this.repository.Add(entity);
            this.repository.Flush();
            return entity;
        }

        public MDM.SourceSystem CreateBasicEntityWithOneMapping()
        {
            var endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<SourceSystem>();

            var endurMapping = new SourceSystemMapping()
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

        public EnergyTrading.Mdm.Contracts.SourceSystem MakeChangeToContract(EnergyTrading.Mdm.Contracts.SourceSystem currentContract)
        {
            this.AddDetailsToContract(currentContract);
            currentContract.MdmSystemData.StartDate = currentContract.MdmSystemData.StartDate.Value.AddDays(2);
            return currentContract;
        }

        public EnergyTrading.Mdm.Contracts.SourceSystem CreateContractForEntityCreation()
        {
            var contract = new EnergyTrading.Mdm.Contracts.SourceSystem();
            this.AddDetailsToContract(contract);
            return contract;
        }

        public void CreateSearch(Search search, SourceSystem entity1, SourceSystem entity2)
        {
            this.CreateSearchData(search, entity1, entity2) ;
        }

        partial void CreateSearchData(Search search, SourceSystem entity1, SourceSystem entity2);

        partial void AddDetailsToContract(EnergyTrading.Mdm.Contracts.SourceSystem contract);

        partial void AddDetailsToEntity(MDM.SourceSystem entity, DateTime startDate, DateTime endDate);

    }
}
