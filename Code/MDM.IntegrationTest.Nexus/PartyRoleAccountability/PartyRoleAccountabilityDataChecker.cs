namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class PartyRoleAccountabilityDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.PartyRoleAccountability contract, MDM.PartyRoleAccountability entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.IsTrue(contract.Details.SourcePartyRole == null && entity.SourcePartyRole == null || contract.Details.SourcePartyRole.NexusId().Value == entity.SourcePartyRole.Id);
            Assert.IsTrue(contract.Details.TargetPartyRole == null && entity.TargetPartyRole == null || contract.Details.TargetPartyRole.NexusId().Value == entity.TargetPartyRole.Id);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.PartyRoleAccountability contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.PartyRoleAccountability>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.PartyRoleAccountability contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.PartyRoleAccountability>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}


