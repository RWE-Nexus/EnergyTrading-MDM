namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class MarketConfiguration : EntityTypeConfiguration<Market>
    {
        public MarketConfiguration()
        {
            this.ToTable("Market");
            this.Property(x => x.Id).HasColumnName("MarketId");
            this.Property(x => x.Name);
            this.HasRequired(x => x.Commodity).WithMany().Map(s => s.MapKey("CommodityId"));
            this.HasRequired(x => x.Location).WithMany().Map(s => s.MapKey("LocationId"));
            this.HasRequired(x => x.Calendar).WithMany().Map(s => s.MapKey("CalendarId"));
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Currency);
            this.Property(x => x.TradeUnits);
            this.Property(x => x.TradeUnitsRate);
            this.Property(x => x.NominationUnits);
            this.Property(x => x.PriceUnits);
            this.Property(x => x.DeliveryRate);
            this.Property(x => x.IncoTerms);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}
