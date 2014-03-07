namespace EnergyTrading.MDM.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    public static class ProductComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.Product contract, MDM.Product entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.CalendarRule, entity.CalendarRule);
            Assert.AreEqual(contract.Details.Market.NexusId(), entity.Market.Id);
            Assert.AreEqual(contract.Details.LotSize, entity.LotSize);
            Assert.AreEqual(contract.Details.Shape.NexusId(), entity.Shape.Id);
            Assert.AreEqual(contract.Details.CommodityInstrumentType.NexusId(), entity.CommodityInstrumentType.Id);
            Assert.AreEqual(contract.Details.DefaultCurve.NexusId(), entity.DefaultCurve.Id);
        }
    }
}