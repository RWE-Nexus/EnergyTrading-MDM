namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class InstrumentTypeOverrideDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride contract, MDM.InstrumentTypeOverride entity)
        {
            Assert.AreEqual(contract.Details.ProductType.Identifier.Identifier, entity.ProductType.Id.ToString());
            Assert.AreEqual(contract.Details.Broker.Identifier.Identifier, entity.Broker.Id.ToString());
            Assert.AreEqual(contract.Details.CommodityInstrumentType.Identifier.Identifier, entity.CommodityInstrumentType.Id.ToString());
            Assert.AreEqual(contract.Details.InstrumentSubType, entity.InstrumentSubType);
            Assert.AreEqual(contract.Details.ProductTenorType.Identifier.Identifier, entity.ProductTenorType.Id.ToString());
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.InstrumentTypeOverride>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.InstrumentTypeOverride>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
