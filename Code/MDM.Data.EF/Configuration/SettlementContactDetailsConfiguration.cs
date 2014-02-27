namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class SettlementContactDetailsConfiguration : EntityTypeConfiguration<SettlementContactDetails>
    {
        public SettlementContactDetailsConfiguration()
        {
            this.Map<SettlementContactDetails>(
                y =>
                    {
                        y.MapInheritedProperties();
                        y.Requires("PartyAccountabilityDetailsClass").HasValue("SettlementContactDetails").IsRequired();
                        y.ToTable("PartyAccountabilityDetails");
                });

            this.HasRequired(x => x.CommodityInstrumentType).WithMany().Map(x => x.MapKey("CommodityInstrumentTypeId"));
        }
    }
}
