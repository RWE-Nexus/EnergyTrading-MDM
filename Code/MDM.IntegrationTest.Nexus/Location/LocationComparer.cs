namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class LocationComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.Location contract, Location entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Type, entity.Type);
        }
    }
}
