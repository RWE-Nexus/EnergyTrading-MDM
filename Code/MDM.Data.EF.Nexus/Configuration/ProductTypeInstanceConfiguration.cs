namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ProductTypeInstanceConfiguration : EntityTypeConfiguration<ProductTypeInstance>
    {
        public ProductTypeInstanceConfiguration()
        {
            this.ToTable("ProductTypeInstance");
            this.Property(x => x.Id).HasColumnName("ProductTypeInstanceId");
            //this.HasMany(x => x.Details);
            this.HasRequired(x => x.ProductType).WithMany().Map(s => s.MapKey("ProductTypeId"));
            this.Property(x => x.Name);
            this.Property(x => x.ShortName);
            this.Property(x => x.DeliveryPeriod);
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
