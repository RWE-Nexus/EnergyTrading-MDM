namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class CommodityFeeTypeDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.CommodityFeeType contract, MDM.CommodityFeeType entity)
        {
            Assert.AreEqual(contract.Details.FeeType.Identifier.Identifier, entity.FeeType.Id.ToString());
            Assert.AreEqual(contract.Details.Commodity.Identifier.Identifier, entity.Commodity.Id.ToString());
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.CommodityFeeType contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.CommodityFeeType>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.CommodityFeeType contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.CommodityFeeType>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
