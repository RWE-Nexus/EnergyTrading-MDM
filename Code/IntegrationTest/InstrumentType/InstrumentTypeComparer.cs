namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class InstrumentTypeComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.InstrumentType contract, InstrumentType entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
        }
    }
}