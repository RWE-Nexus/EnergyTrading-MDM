namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    public static class MarketComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.Market contract, MDM.Market entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Calendar.NexusId().Value, entity.Calendar.Id);
            Assert.AreEqual(contract.Details.Commodity.NexusId().Value, entity.Commodity.Id);
            Assert.AreEqual(contract.Details.Currency, entity.Currency);
            Assert.AreEqual(contract.Details.TradeUnits, entity.TradeUnits);
            Assert.AreEqual(contract.Details.NominationUnits, entity.TradeUnits);
        }
    }
}