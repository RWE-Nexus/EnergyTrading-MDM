namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class DimensionDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.Dimension contract, MDM.Dimension entity)
        {
			Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Description, entity.Description);
            Assert.AreEqual(contract.Details.ElectricCurrentDimension, entity.ElectricCurrentDimension);
            Assert.AreEqual(contract.Details.LengthDimension, entity.LengthDimension);
            Assert.AreEqual(contract.Details.MassDimension, entity.MassDimension);
            Assert.AreEqual(contract.Details.LuminousIntensityDimension, entity.LuminousIntensityDimension);
            Assert.AreEqual(contract.Details.TimeDimension, entity.TimeDimension);
            Assert.AreEqual(contract.Details.TemperatureDimension, entity.TemperatureDimension);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.Dimension contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.Dimension>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.Dimension contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.Dimension>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
