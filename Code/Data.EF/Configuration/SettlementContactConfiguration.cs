namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class SettlementContactConfiguration : EntityTypeConfiguration<MDM.SettlementContact>
    {
        public SettlementContactConfiguration()
        {
            this.Map<SettlementContact>(y =>
            {
                y.MapInheritedProperties();
                y.Requires("PartyAccountabilityClass").HasValue("SettlementContact").IsRequired();
                y.ToTable("PartyAccountability");
            });

            this.HasOptional(x => x.CommodityInstrumentType).WithMany().Map(s => s.MapKey("CommodityInstrumentTypeId"));
            this.HasOptional(x => x.Location).WithMany().Map(s => s.MapKey("LocationId"));
        }
    }
}
