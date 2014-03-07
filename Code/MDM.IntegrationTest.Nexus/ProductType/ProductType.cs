namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    public static class ProductTypeComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.ProductType contract, MDM.ProductType entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.DeliveryRangeType, entity.DeliveryRangeType);
            Assert.AreEqual(contract.Details.IsRelative, entity.IsRelative);
            Assert.AreEqual(contract.Details.Product.NexusId(), entity.Product.Id);
        }
    }
}