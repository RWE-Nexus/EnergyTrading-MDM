namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ProductTypeMappingConfiguration : EntityTypeConfiguration<ProductTypeMapping>
    {
        public ProductTypeMappingConfiguration()
        {
            this.ToTable("ProductTypeMapping");

            this.Property(x => x.Id).HasColumnName("ProductTypeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ProductType).WithMany(y => y.Mappings).Map(x => x.MapKey("ProductTypeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}