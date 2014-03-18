namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class ProductTenorTypeMappingConfiguration : EntityTypeConfiguration<ProductTenorTypeMapping>
    {
        public ProductTenorTypeMappingConfiguration()
        {
            this.ToTable("ProductTenorTypeMapping");

            this.Property(x => x.Id).HasColumnName("ProductTenorTypeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ProductTenorType).WithMany(y => y.Mappings).Map(x => x.MapKey("ProductTenorTypeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}