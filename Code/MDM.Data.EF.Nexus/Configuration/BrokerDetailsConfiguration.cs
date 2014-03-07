namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class BrokerDetailsConfiguration : EntityTypeConfiguration<BrokerDetails>
    {
        public BrokerDetailsConfiguration()
        {
            this.Map<BrokerDetails>(
                y =>
                    {
                        y.MapInheritedProperties();
                        y.Requires("PartyRoleDetailsClass").HasValue("BrokerDetails").IsRequired();
                        y.ToTable("PartyRoleDetails");
                });

            this.Property(x => x.Phone).HasColumnName("BrokerPhone");
            this.Property(x => x.Fax).HasColumnName("BrokerFax");
            this.Property(x => x.Rate).HasPrecision(18, 10);
        }
    }
}