namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class PartyAccountabilityDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.PartyAccountability contract, MDM.PartyAccountability entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.IsTrue(contract.Details.SourceParty == null && entity.SourceParty == null || contract.Details.SourceParty.NexusId().Value == entity.SourceParty.Id);
            Assert.IsTrue(contract.Details.SourcePerson == null && entity.SourcePerson == null || contract.Details.SourcePerson.NexusId().Value == entity.SourcePerson.Id);
            Assert.IsTrue(contract.Details.TargetPerson == null && entity.TargetPerson == null || contract.Details.TargetPerson.NexusId().Value == entity.TargetPerson.Id);
            Assert.IsTrue(contract.Details.TargetParty == null && entity.TargetParty == null || contract.Details.TargetParty.NexusId().Value == entity.TargetParty.Id);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.PartyAccountability contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.PartyAccountability>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.PartyAccountability contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.PartyAccountability>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}


