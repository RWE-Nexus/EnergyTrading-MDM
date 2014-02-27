namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class DimensionMappingConfiguration : EntityTypeConfiguration<DimensionMapping>
    {
        public DimensionMappingConfiguration()
        {
            this.ToTable("DimensionMapping");

            this.Property(x => x.Id).HasColumnName("DimensionMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Dimension).WithMany(y => y.Mappings).Map(x => x.MapKey("DimensionId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}