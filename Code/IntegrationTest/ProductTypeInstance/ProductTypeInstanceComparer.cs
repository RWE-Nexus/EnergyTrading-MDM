namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    public static class ProductTypeInstanceComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.ProductTypeInstance contract, MDM.ProductTypeInstance entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Delivery.StartDate, entity.Delivery.Start);
            Assert.AreEqual(contract.Details.Delivery.EndDate, entity.Delivery.Finish);
            Assert.AreEqual(contract.Details.ProductType.NexusId(), entity.ProductType.Id);
        }
    }
}