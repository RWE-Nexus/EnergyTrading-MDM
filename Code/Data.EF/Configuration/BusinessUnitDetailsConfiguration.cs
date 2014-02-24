namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class BusinessUnitDetailsConfiguration : EntityTypeConfiguration<BusinessUnitDetails>
    {
        public BusinessUnitDetailsConfiguration()
        {
            this.Map<BusinessUnitDetails>(
                y =>
                    {
                        y.MapInheritedProperties();
                        y.Requires("PartyRoleDetailsClass").HasValue("BusinessUnitDetails").IsRequired();
                        y.ToTable("PartyRoleDetails");
                    });

            this.Property(x => x.Phone).HasColumnName("BusinessUnitPhone");
            this.Property(x => x.Fax).HasColumnName("BusinessUnitFax");
            this.Property(x => x.AccountType).HasColumnName("BusinessUnitAccountType");
            this.Property(x => x.Address).HasColumnName("BusinessUnitAddress");
            this.Property(x => x.Status).HasColumnName("BusinessUnitStatus");
            this.HasOptional(x => x.TaxLocation).WithMany().Map(x => x.MapKey("TaxLocationId"));
        }
    }
}