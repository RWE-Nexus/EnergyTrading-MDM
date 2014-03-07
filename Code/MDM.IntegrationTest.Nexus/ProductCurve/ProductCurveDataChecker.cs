namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class ProductCurveDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.ProductCurve contract, MDM.ProductCurve entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Curve.NexusId(), entity.Curve.Id);
            Assert.AreEqual(contract.Details.Product.NexusId(), entity.Product.Id);
            Assert.AreEqual(contract.Details.ProductCurveType, entity.ProductCurveType);
            Assert.AreEqual(contract.Details.ProjectionMethod, entity.ProjectionMethod);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.ProductCurve contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.ProductCurve>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.ProductCurve contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.ProductCurve>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}
