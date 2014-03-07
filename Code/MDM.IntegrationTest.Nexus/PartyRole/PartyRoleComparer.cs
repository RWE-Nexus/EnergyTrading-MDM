namespace RWEST.Nexus.MDM.Test
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class PartyRoleComparer
    {
        public static void Compare(Contracts.PartyRole contract, MDM.PartyRole entity)
        {
            PartyRoleDetails detailsToCompare = entity.Details[0]; 

            if (contract.Nexus != null)
            {
                detailsToCompare = entity.Details.Where(details => details.Validity.Start == contract.Nexus.StartDate).First();
            }

            Assert.AreEqual(contract.Details.Name, detailsToCompare.Name);
        }
    }
}
