namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class CommodityComparer
    {

        public static void Compare(RWEST.Nexus.MDM.Contracts.Commodity contract, Commodity entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
        }
    }
}
