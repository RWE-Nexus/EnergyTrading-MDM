namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class LegalEntityDetailsConfiguration : EntityTypeConfiguration<LegalEntityDetails>
    {
        public LegalEntityDetailsConfiguration()
        {
            this.Map<LegalEntityDetails>(
                y =>
                {
                    y.MapInheritedProperties();
                    y.Requires("PartyRoleDetailsClass").HasValue("LegalEntityDetails").IsRequired();
                    y.ToTable("PartyRoleDetails");
                });

            this.Property(x => x.RegisteredName).HasColumnName("LegalEntityRegisteredName");
            this.Property(x => x.RegistrationNumber).HasColumnName("LegalEntityRegistrationNumber");
            this.Property(x => x.Address).HasColumnName("LegalEntityAddress");
            this.Property(x => x.Website).HasColumnName("LegalEntityWebsite");
            this.Property(x => x.Email).HasColumnName("LegalEntityEmail");
            this.Property(x => x.Fax).HasColumnName("LegalEntityFax");
            this.Property(x => x.Phone).HasColumnName("LegalEntityPhone");
            this.Property(x => x.CountryOfIncorporation).HasColumnName("LegalEntityCountryOfInc");
            this.Property(x => x.PartyStatus).HasColumnName("LegalEntityPartyStatus");
            this.Property(x => x.InvoiceSetup).HasColumnName("LegalEntityInvoiceSetup");
            this.Property(x => x.CustomerAddress).HasColumnName("LegalEntityCustomerAddress");
            this.Property(x => x.VendorAddress).HasColumnName("LegalEntityVendorAddress");
        }
    }
}