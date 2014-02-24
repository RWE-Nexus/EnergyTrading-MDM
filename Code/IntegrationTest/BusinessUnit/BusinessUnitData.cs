namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    public static class BusinessUnitData
    {
        private static readonly DbSetRepository repository;

        private static DateTime baseDate;

        static BusinessUnitData()
        {
            repository = ObjectScript.Repository;
        }

        public static BusinessUnit CreateBasicEntity()
        {
            var entity = ObjectMother.Create<BusinessUnit>();
            repository.Add(entity);
            repository.Flush();
            return entity;
        }

        public static BusinessUnit CreateBasicEntityWithOneMapping()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();

            var entity = ObjectMother.Create<BusinessUnit>();

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

        public static RWEST.Nexus.MDM.Contracts.BusinessUnit CreateContractForEntityCreation()
        {
            var contract = new RWEST.Nexus.MDM.Contracts.BusinessUnit();
            AddDetailsToContract(contract);
            return contract;
        }

        public static BusinessUnit CreateEntityWithTwoDetailsAndTwoMappings()
        {
            SourceSystem endur = repository.Queryable<SourceSystem>().Where(system => system.Name == "Endur").First();
            SourceSystem trayport =
                repository.Queryable<SourceSystem>().Where(system => system.Name == "Trayport").First();

            var entity = new BusinessUnit();
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

        public static void CreateSearch(Search search, BusinessUnit entity1, BusinessUnit entity2)
        {
            CreateSearchData(search, entity1, entity2);
        }

        public static RWEST.Nexus.MDM.Contracts.BusinessUnit MakeChangeToContract(RWEST.Nexus.MDM.Contracts.BusinessUnit currentContract)
        {
            var entity = ObjectMother.Create<BusinessUnit>();
            repository.Add(entity);
            repository.Flush();

            currentContract.Details = new RWEST.Nexus.MDM.Contracts.BusinessUnitDetails()
            {
                Name = entity.LatestDetails.Name,
                Phone = (entity.LatestDetails as BusinessUnitDetails).Phone,
                Fax = (entity.LatestDetails as BusinessUnitDetails).Fax,
                AccountType = (entity.LatestDetails as BusinessUnitDetails).AccountType,
                Address = (entity.LatestDetails as BusinessUnitDetails).Address,
                Status = (entity.LatestDetails as BusinessUnitDetails).Status,
                TaxLocation = (entity.LatestDetails as BusinessUnitDetails).TaxLocation.CreateNexusEntityId(() => (entity.LatestDetails as BusinessUnitDetails).TaxLocation.Name)
            };

            currentContract.Nexus.StartDate = currentContract.Nexus.StartDate.Value.AddDays(2);
            return currentContract;
        }

        private static void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.BusinessUnit contract)
        {
            var entity = ObjectMother.Create<BusinessUnit>();
            repository.Add(entity);
            repository.Flush();

            contract.Details = new RWEST.Nexus.MDM.Contracts.BusinessUnitDetails()
            {
                Name = entity.LatestDetails.Name,
                Phone = (entity.LatestDetails as BusinessUnitDetails).Phone,
                Fax = (entity.LatestDetails as BusinessUnitDetails).Fax,
                TaxLocation = (entity.LatestDetails as BusinessUnitDetails).TaxLocation.CreateNexusEntityId(() => (entity.LatestDetails as BusinessUnitDetails).TaxLocation.Name)
            };
            contract.Party = entity.Party.CreateNexusEntityId(() => "");
        }

        private static void AddDetailsToEntity(BusinessUnit entity, DateTime startDate, DateTime endDate)
        {
            var newEntity = ObjectMother.Create<BusinessUnit>();
            
            entity.PartyRoleType = newEntity.PartyRoleType;
            entity.AddDetails(new BusinessUnitDetails()
            {
                Name = newEntity.Details[0].Name,
                Phone = ((BusinessUnitDetails)newEntity.Details[0]).Phone,
                Fax = ((BusinessUnitDetails)newEntity.Details[0]).Fax,
                TaxLocation = ((BusinessUnitDetails)newEntity.Details[0]).TaxLocation,
                AccountType = ((BusinessUnitDetails)newEntity.Details[0]).AccountType,
                Address = ((BusinessUnitDetails)newEntity.Details[0]).Address,
                Status = ((BusinessUnitDetails)newEntity.Details[0]).Status, 
            });
        }

        private static void CreateSearchData(Search search, BusinessUnit entity1, BusinessUnit entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.LatestDetails.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.LatestDetails.Name);
        }
    }
}
