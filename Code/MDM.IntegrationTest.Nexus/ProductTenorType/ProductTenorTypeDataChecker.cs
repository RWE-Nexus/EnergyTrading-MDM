namespace EnergyTrading.MDM.Test
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;

    public static class ProductTenorTypeDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.ProductTenorType contract, MDM.ProductTenorType entity)
        {
            Assert.AreEqual(contract.Details.Product.Name, entity.Product.Name);
            Assert.AreEqual(contract.Details.TenorType.Name, entity.TenorType.Name);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.ProductTenorType contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.ProductTenorType>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.ProductTenorType contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.ProductTenorType>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
