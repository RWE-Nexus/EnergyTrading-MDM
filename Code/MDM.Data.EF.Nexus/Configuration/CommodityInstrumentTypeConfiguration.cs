namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class CommodityInstrumentTypeConfiguration : EntityTypeConfiguration<CommodityInstrumentType>
    {
        public CommodityInstrumentTypeConfiguration()
        {
            this.ToTable("CommodityInstrumentType");
            this.Property(x => x.Id).HasColumnName("CommodityInstrumentTypeId");
            this.HasRequired(x => x.Commodity).WithMany().Map(s => s.MapKey("CommodityId"));
            this.HasOptional(x => x.InstrumentType).WithMany().Map(s => s.MapKey("InstrumentTypeId"));
            this.Property(x => x.InstrumentDelivery);
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}