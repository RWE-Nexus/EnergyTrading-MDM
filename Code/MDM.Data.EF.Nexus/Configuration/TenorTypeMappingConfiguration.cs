namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class TenorTypeMappingConfiguration : EntityTypeConfiguration<TenorTypeMapping>
    {
        public TenorTypeMappingConfiguration()
        {
            this.ToTable("TenorTypeMapping");

            this.Property(x => x.Id).HasColumnName("TenorTypeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.TenorType).WithMany(y => y.Mappings).Map(x => x.MapKey("TenorTypeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}