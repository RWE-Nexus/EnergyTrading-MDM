namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class CounterpartyDetailsConfiguration : EntityTypeConfiguration<CounterpartyDetails>
    {
        public CounterpartyDetailsConfiguration()
        {
            this.Map<CounterpartyDetails>(
                y =>
                    {
                        y.MapInheritedProperties();
                        y.Requires("PartyRoleDetailsClass").HasValue("CounterpartyDetails").IsRequired();
                        y.ToTable("PartyRoleDetails");
                });

            this.Property(x => x.Phone).HasColumnName("CounterpartyPhone");
            this.Property(x => x.Fax).HasColumnName("CounterpartyFax");
            this.Property(x => x.ShortName);
            //this.HasOptional(x => x.TaxLocation).WithMany().Map(x => x.MapKey("TaxLocationId"));
        }
    }
}
