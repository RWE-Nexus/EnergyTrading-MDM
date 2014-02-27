namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ProductCurveMappingConfiguration : EntityTypeConfiguration<ProductCurveMapping>
    {
        public ProductCurveMappingConfiguration()
        {
            this.ToTable("ProductCurveMapping");

            this.Property(x => x.Id).HasColumnName("ProductCurveMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ProductCurve).WithMany(y => y.Mappings).Map(x => x.MapKey("ProductCurveId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}