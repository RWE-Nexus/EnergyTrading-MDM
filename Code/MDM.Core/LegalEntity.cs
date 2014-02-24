namespace EnergyTrading.MDM
{
    public class LegalEntity : PartyRole
    {
        protected override void CopyAdditionalDetails(PartyRoleDetails details)
        {
            var latestDetails = (LegalEntityDetails)LatestDetails;
            var newDetails = (LegalEntityDetails)details;

            latestDetails.RegisteredName = newDetails.RegisteredName;
            latestDetails.RegistrationNumber = newDetails.RegistrationNumber;
            latestDetails.Address = newDetails.Address;
            latestDetails.Website = newDetails.Website;
            latestDetails.Email = newDetails.Email;
            latestDetails.Fax = newDetails.Fax;
            latestDetails.Phone = newDetails.Phone;
            latestDetails.CountryOfIncorporation = newDetails.CountryOfIncorporation;
            latestDetails.PartyStatus = newDetails.PartyStatus;
            latestDetails.CustomerAddress = newDetails.CustomerAddress;
            latestDetails.VendorAddress = newDetails.VendorAddress;
            latestDetails.InvoiceSetup = newDetails.InvoiceSetup;
        }
    }
}
