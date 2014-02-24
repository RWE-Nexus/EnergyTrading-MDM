namespace EnergyTrading.MDM.Test
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;

    public static class TenorDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.Tenor contract, MDM.Tenor entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.ShortName, entity.ShortName);
            Assert.AreEqual(contract.Details.TenorType.Name, entity.TenorType.Name);
            Assert.AreEqual(contract.Details.IsRelative, entity.IsRelative);
            Assert.AreEqual(contract.Details.DeliveryRangeType, entity.DeliveryRangeType);
            Assert.AreEqual(contract.Details.DeliveryPeriod, entity.DeliveryPeriod);
            Assert.AreEqual(contract.Details.Delivery.StartDate, entity.Delivery.Start);
            Assert.AreEqual(contract.Details.Delivery.EndDate, entity.Delivery.Finish);
            Assert.AreEqual(contract.Details.Traded.StartDate, entity.Traded.Start);
            Assert.AreEqual(contract.Details.Traded.EndDate, entity.Traded.Finish);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.Tenor contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.Tenor>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.Tenor contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.Tenor>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
