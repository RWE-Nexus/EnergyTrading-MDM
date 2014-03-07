namespace EnergyTrading.MDM
{
    public class LegalEntityDetails : PartyRoleDetails
    {
        public string RegisteredName { get; set; }

        public string RegistrationNumber { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string Phone { get; set; }

        public string CountryOfIncorporation { get; set; }

        public string PartyStatus { get; set; }

        public string CustomerAddress { get; set; }

        public string InvoiceSetup { get; set; }

        public string VendorAddress { get; set; }
    }
}
