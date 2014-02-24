namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ProductCurveConfiguration : EntityTypeConfiguration<ProductCurve>
    {
        public ProductCurveConfiguration()
        {
            this.ToTable("ProductCurve");
            this.Property(x => x.Id).HasColumnName("ProductCurveId");

            this.Property(x => x.Name);
            this.HasRequired(x => x.Curve).WithMany().Map(s => s.MapKey("CurveId"));
            this.HasRequired(x => x.Product).WithMany(y => y.ProductCurves).Map(s => s.MapKey("ProductId"));
            this.Property(x => x.ProductCurveType);
            this.Property(x => x.ProjectionMethod);
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}