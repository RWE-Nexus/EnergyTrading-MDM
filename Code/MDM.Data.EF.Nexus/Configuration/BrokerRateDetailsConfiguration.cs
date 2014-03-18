namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BrokerRateDetailsConfiguration : EntityTypeConfiguration<BrokerRateDetails>
    {
        public BrokerRateDetailsConfiguration()
        {
            this.Property(x => x.Id).HasColumnName("BrokerRateDetailsId");
            this.HasRequired(x => x.BrokerRate).WithMany(y => y.Details).Map(x => x.MapKey("BrokerRateId"));
            this.HasRequired(x => x.Broker).WithMany().Map(s => s.MapKey("BrokerId"));
            this.HasRequired(x => x.Desk).WithMany().Map(s => s.MapKey("DeskId"));
            this.HasRequired(x => x.CommodityInstrumentType).WithMany().Map(s => s.MapKey("CommodityInstrumentTypeId"));
            this.HasOptional(x => x.Location).WithMany().Map(s => s.MapKey("LocationId"));
            this.HasOptional(x => x.ProductType).WithMany().Map(s => s.MapKey("ProductTypeId"));
            this.Property(x => x.PartyAction).HasColumnName("PartyAction").IsRequired();
            this.Property(x => x.RateType).HasColumnName("RateType");
            this.Property(x => x.Currency).HasColumnName("Currency");
            this.Property(x => x.Rate).HasPrecision(18, 10).IsRequired();
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
            
        }
    }
}