using System;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.Test
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class BrokerRateComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.BrokerRate contract, MDM.BrokerRate entity)
        {
            BrokerRateDetails detailsToCompare = entity.Details[0];

            if (contract.Nexus != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.Nexus.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Broker.Identifier.Identifier, detailsToCompare.Broker.Id.ToString());
            Assert.AreEqual(contract.Details.CommodityInstrumentType.Identifier.Identifier, detailsToCompare.CommodityInstrumentType.Id.ToString());
            Assert.AreEqual(contract.Details.Desk.Identifier.Identifier, detailsToCompare.Desk.Id.ToString());
            Assert.AreEqual(contract.Details.Location.Identifier.Identifier, detailsToCompare.Location.Id.ToString());
            Assert.AreEqual(contract.Details.ProductType.Identifier.Identifier, detailsToCompare.ProductType.Id.ToString());
            Assert.AreEqual(contract.Details.PartyAction, (PartyAction)Enum.ToObject(typeof(PartyAction), detailsToCompare.PartyAction));
            Assert.AreEqual(contract.Details.Rate, detailsToCompare.Rate);
        }
    }
}