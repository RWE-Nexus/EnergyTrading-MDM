namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class ProductTenorTypeConfiguration : EntityTypeConfiguration<ProductTenorType>
    {
        public ProductTenorTypeConfiguration()
        {
            this.ToTable("ProductTenorType");
            this.Property(x => x.Id).HasColumnName("ProductTenorTypeId");

            this.HasRequired(x => x.Product).WithMany().Map(s => s.MapKey("ProductId"));
            this.HasRequired(x => x.TenorType).WithMany().Map(s => s.MapKey("TenorTypeId"));

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}