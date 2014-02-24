namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class ProductScotaDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.ProductScota contract, MDM.ProductScota entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Product.NexusId(), entity.Product.Id);
            Assert.AreEqual(contract.Details.ScotaDeliveryPoint.NexusId(), entity.ScotaDeliveryPoint.Id);
            Assert.AreEqual(contract.Details.ScotaOrigin.NexusId(), entity.ScotaOrigin.Id);
            Assert.AreEqual(contract.Details.ScotaContract, entity.ScotaContract);
            Assert.AreEqual(contract.Details.ScotaRss, entity.ScotaRss);
            Assert.AreEqual(contract.Details.ScotaVersion, entity.ScotaVersion);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.ProductScota contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.ProductScota>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.ProductScota contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.ProductScota>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
