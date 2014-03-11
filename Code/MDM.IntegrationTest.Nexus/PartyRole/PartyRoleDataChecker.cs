namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class PartyRoleDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.PartyRole contract, MDM.PartyRole entity)
        {
            MDM.PartyRoleDetails detailsToCompare = entity.Details[0]; 

            if (contract.Nexus != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.Nexus.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Name, detailsToCompare.Name);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.PartyRole contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.PartyRole>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.PartyRole contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.PartyRole>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

