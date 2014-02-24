using Microsoft.VisualStudio.TestTools.UnitTesting;
using RWEST.Nexus.MDM.Contracts;

namespace RWEST.Nexus.MDM.Test
{
    public static class CurveComparer
    {
        public static void Compare(Contracts.Curve contract, MDM.Curve entity)
        {
            Assert.AreEqual(contract.Details.Name, entity.Name);
            Assert.AreEqual(contract.Details.Commodity.NexusId().Value, entity.Commodity.Id);
            Assert.AreEqual(contract.Details.Originator.NexusId().Value, entity.Originator.Id);
            Assert.AreEqual(contract.Details.Location.NexusId().Value, entity.Location.Id);
            Assert.AreEqual(contract.Details.CommodityUnit, entity.CommodityUnit);
            Assert.AreEqual(contract.Details.CurveType, entity.Type);
            Assert.AreEqual(contract.Details.DefaultSpread, entity.DefaultSpread);
            Assert.AreEqual(contract.Details.Currency, entity.Currency);

        }
    }
}

