namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ShapeMappingConfiguration : EntityTypeConfiguration<ShapeMapping>
    {
        public ShapeMappingConfiguration()
        {
            this.ToTable("ShapeMapping");

            this.Property(x => x.Id).HasColumnName("ShapeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Shape).WithMany(y => y.Mappings).Map(x => x.MapKey("ShapeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}