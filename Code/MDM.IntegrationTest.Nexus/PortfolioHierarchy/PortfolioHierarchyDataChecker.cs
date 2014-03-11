namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class PortfolioHierarchyDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.PortfolioHierarchy contract, MDM.PortfolioHierarchy entity)
        {
            Assert.AreEqual(contract.Details.ChildPortfolio.Identifier.Identifier, entity.ChildPortfolio.Id.ToString());
            Assert.AreEqual(contract.Details.ParentPortfolio.Identifier.Identifier, entity.ParentPortfolio.Id.ToString());
            Assert.AreEqual(contract.Details.Hierarchy.Identifier.Identifier, entity.Hierarachy.Id.ToString());
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.PortfolioHierarchy contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.PortfolioHierarchy>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.PortfolioHierarchy contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.PortfolioHierarchy>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
