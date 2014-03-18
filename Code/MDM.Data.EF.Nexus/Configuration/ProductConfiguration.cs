namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            this.ToTable("Product");
            this.Property(x => x.Id).HasColumnName("ProductId");
            this.Property(x => x.CalendarRule);
            this.Property(x => x.Name);
            this.HasRequired(x => x.Market).WithMany().Map(s => s.MapKey("MarketId"));
            this.HasOptional(x => x.Exchange).WithMany().Map(s => s.MapKey("ExchangeId"));
            this.HasOptional(x => x.Shape).WithMany().Map(s => s.MapKey("ShapeId"));
            this.HasOptional(x => x.CommodityInstrumentType).WithMany().Map(s => s.MapKey("CommodityInstrumentTypeId"));
            this.HasOptional(x => x.DefaultCurve).WithMany().Map(s => s.MapKey("CurveId"));
            this.Property(x => x.LotSize);
            this.Property(x => x.IncoTerms);
            this.Property(x => x.InstrumentSubType);
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
            this.HasMany(x => x.ProductCurves);
        }
    }
}
