namespace EnergyTrading.MDM.Test
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;

    public static class TenorTypeDataChecker
    {
        public static void CompareContractWithEntityDetails(TenorType contract, MDM.TenorType entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.ShortName, entity.ShortName);
            //Assert.AreEqual(contract.Details.Traded.StartDate, entity.Traded.Start);
            //Assert.AreEqual(contract.Details.Traded.EndDate, entity.Traded.Finish);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.TenorType contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.TenorType>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.TenorType contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.TenorType>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
