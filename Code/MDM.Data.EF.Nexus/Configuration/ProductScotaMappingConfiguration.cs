namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ProductScotaMappingConfiguration : EntityTypeConfiguration<ProductScotaMapping>
    {
        public ProductScotaMappingConfiguration()
        {
            this.ToTable("ProductScotaMapping");

            this.Property(x => x.Id).HasColumnName("ProductScotaMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ProductScota).WithMany(y => y.Mappings).Map(x => x.MapKey("ProductScotaId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}