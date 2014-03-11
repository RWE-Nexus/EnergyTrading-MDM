namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class CommodityInstrumentTypeDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.CommodityInstrumentType contract, MDM.CommodityInstrumentType entity)
        {
            Assert.AreEqual(contract.Details.InstrumentType.Identifier.Identifier, entity.InstrumentType.Id.ToString());
            Assert.AreEqual(contract.Details.Commodity.Identifier.Identifier, entity.Commodity.Id.ToString());
            Assert.AreEqual(contract.Details.InstrumentDelivery, entity.InstrumentDelivery);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.CommodityInstrumentType contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.CommodityInstrumentType>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.CommodityInstrumentType contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.CommodityInstrumentType>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
