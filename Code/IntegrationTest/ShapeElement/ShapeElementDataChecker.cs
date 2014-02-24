namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class ShapeElementDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.ShapeElement contract, MDM.ShapeElement entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Period.StartDate, entity.Period.Start);
            Assert.AreEqual(contract.Details.Period.EndDate, entity.Period.Finish);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.ShapeElement contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.ShapeElement>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.ShapeElement contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.ShapeElement>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
