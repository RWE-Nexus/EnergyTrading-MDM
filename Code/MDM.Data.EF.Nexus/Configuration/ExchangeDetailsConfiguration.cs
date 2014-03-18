namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class ExchangeDetailsConfiguration : EntityTypeConfiguration<ExchangeDetails>
    {
        public ExchangeDetailsConfiguration()
        {
            this.Map<ExchangeDetails>(
                y =>
                    {
                        y.MapInheritedProperties();
                        y.Requires("PartyRoleDetailsClass").HasValue("ExchangeDetails").IsRequired();
                        y.ToTable("PartyRoleDetails");
                });

            this.Property(x => x.Phone).HasColumnName("ExchangePhone");
            this.Property(x => x.Fax).HasColumnName("ExchangeFax");
        }
    }
}
