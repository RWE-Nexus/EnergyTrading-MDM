namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;

    public class LegalEntityDetailsMapper : Mapper<OpenNexus.MDM.Contracts.LegalEntityDetails, MDM.LegalEntityDetails>
    {
        public override void Map(OpenNexus.MDM.Contracts.LegalEntityDetails source, MDM.LegalEntityDetails destination)
        {
            destination.Name = source.Name;
            destination.Address = source.Address;
            destination.CountryOfIncorporation = source.CountryOfIncorporation;
            destination.Email = source.Email;
            destination.Fax = source.Fax;
            destination.PartyStatus = source.PartyStatus;
            destination.Phone = source.Phone;
            destination.RegisteredName = source.RegisteredName;
            destination.RegistrationNumber = source.RegistrationNumber;
            destination.Website = source.Website;
            destination.CustomerAddress = source.CustomerAddress;
            destination.InvoiceSetup = source.InvoiceSetup;
            destination.VendorAddress = source.VendorAddress;
        }
    }
}
