namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class ProductMappingConfiguration : EntityTypeConfiguration<ProductMapping>
    {
        public ProductMappingConfiguration()
        {
            this.ToTable("ProductMapping");

            this.Property(x => x.Id).HasColumnName("ProductMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Product).WithMany(y => y.Mappings).Map(x => x.MapKey("ProductId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}