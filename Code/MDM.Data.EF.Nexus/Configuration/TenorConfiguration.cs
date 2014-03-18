namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class TenorConfiguration : EntityTypeConfiguration<Tenor>
    {
        public TenorConfiguration()
        {
            this.ToTable("Tenor");
            this.Property(x => x.Id).HasColumnName("TenorId");
            this.Property(x => x.Name);
            this.Property(x => x.ShortName);
            this.HasRequired(x => x.TenorType).WithMany().Map(s => s.MapKey("TenorTypeId"));
            this.Property(x => x.IsRelative); 
            this.Property(x => x.DeliveryPeriod);
            this.Property(x => x.DeliveryRangeType);
            this.Property(x => x.Delivery.Start).HasColumnName("DeliveryStart");
            this.Property(x => x.Delivery.Finish).HasColumnName("DeliveryFinish"); 
            this.Property(x => x.Traded.Start).HasColumnName("TradedStart");
            this.Property(x => x.Traded.Finish).HasColumnName("TradedFinish");
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}