namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class BookDefaultDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.BookDefault contract, MDM.BookDefault entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Book.Identifier.Identifier, entity.Book.Id.ToString());
            Assert.AreEqual(contract.Details.Desk.Identifier.Identifier, entity.Desk.Id.ToString());
            Assert.AreEqual(contract.Details.Trader.Identifier.Identifier, entity.Trader.Id.ToString());
            Assert.AreEqual(contract.Details.GfProductMapping, entity.GfProductMapping);
            Assert.AreEqual(contract.Details.DefaultType, entity.DefaultType);
            Assert.AreEqual(contract.Details.PartyRole.Identifier.Identifier, entity.PartyRole.Id.ToString());
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.BookDefault contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.BookDefault>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.BookDefault contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.BookDefault>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
