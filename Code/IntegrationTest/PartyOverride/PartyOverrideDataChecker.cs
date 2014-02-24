namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class PartyOverrideDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.PartyOverride contract, MDM.PartyOverride entity)
        {
            Assert.AreEqual(contract.Details.Broker.Identifier.Identifier, entity.Broker.Id.ToString());
            Assert.AreEqual(contract.Details.CommodityInstrumentType.Identifier.Identifier, entity.CommodityInstrumentType.Id.ToString());
            Assert.AreEqual(contract.Details.MappingValue, entity.MappingValue);
            Assert.AreEqual(contract.Details.Party.Identifier.Identifier, entity.Party.Id.ToString());
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.PartyOverride contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.PartyOverride>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.PartyOverride contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.PartyOverride>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
