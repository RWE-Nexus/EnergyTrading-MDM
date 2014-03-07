namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ProductTypeInstanceMappingConfiguration : EntityTypeConfiguration<ProductTypeInstanceMapping>
    {
        public ProductTypeInstanceMappingConfiguration()
        {
            this.ToTable("ProductTypeInstanceMapping");

            this.Property(x => x.Id).HasColumnName("ProductTypeInstanceMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ProductTypeInstance).WithMany(y => y.Mappings).Map(x => x.MapKey("ProductTypeInstanceId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}