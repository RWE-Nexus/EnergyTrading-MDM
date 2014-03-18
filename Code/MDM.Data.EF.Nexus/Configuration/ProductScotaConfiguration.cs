namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class ProductScotaConfiguration : EntityTypeConfiguration<ProductScota>
    {
        public ProductScotaConfiguration()
        {
            this.ToTable("ProductScota");
            this.Property(x => x.Id).HasColumnName("ProductScotaId");
            
            this.Property(x => x.Name);
            this.HasRequired(x => x.Product).WithMany().Map(s => s.MapKey("ProductId"));
            this.HasOptional(x => x.ScotaDeliveryPoint).WithMany().Map(s => s.MapKey("ScotaDeliveryPointId"));
            this.HasOptional(x => x.ScotaOrigin).WithMany().Map(s => s.MapKey("ScotaOriginId"));
            this.Property(x => x.ScotaContract);
            this.Property(x => x.ScotaRss);
            this.Property(x => x.ScotaVersion);

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}