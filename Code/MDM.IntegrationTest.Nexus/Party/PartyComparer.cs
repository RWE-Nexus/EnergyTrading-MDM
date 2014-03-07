namespace EnergyTrading.MDM.Test
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class PartyComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.Party contract, MDM.Party entity)
        {
            PartyDetails detailsToCompare = entity.Details[0]; 

            if (contract.Nexus != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.Nexus.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Name, detailsToCompare.Name);
            Assert.AreEqual(contract.Details.FaxNumber, detailsToCompare.Fax);
            Assert.AreEqual(contract.Details.TelephoneNumber, detailsToCompare.Phone);
            Assert.AreEqual(contract.Details.Role, detailsToCompare.Role);
        }
    }
}