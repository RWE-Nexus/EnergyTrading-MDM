namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class FeeTypeComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.FeeType contract, MDM.FeeType entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
        }
    }
}