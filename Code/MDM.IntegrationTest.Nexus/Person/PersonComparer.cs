namespace EnergyTrading.MDM.Test
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class PersonComparer
    {
        public static void Compare(RWEST.Nexus.MDM.Contracts.Person contract, MDM.Person entity)
        {
            PersonDetails detailsToCompare = entity.Details[0]; 

            if (contract.Nexus != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.Nexus.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Forename, detailsToCompare.FirstName);
            Assert.AreEqual(contract.Details.Surname, detailsToCompare.LastName);
            Assert.AreEqual(contract.Details.FaxNumber, detailsToCompare.Fax);
            Assert.AreEqual(contract.Details.Role, detailsToCompare.Role);
            Assert.AreEqual(contract.Details.TelephoneNumber, detailsToCompare.Phone);
            Assert.AreEqual(contract.Details.Email, detailsToCompare.Email);
        }
    }
}