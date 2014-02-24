namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ShapeElementMappingConfiguration : EntityTypeConfiguration<ShapeElementMapping>
    {
        public ShapeElementMappingConfiguration()
        {
            this.ToTable("ShapeElementMapping");

            this.Property(x => x.Id).HasColumnName("ShapeElementMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ShapeElement).WithMany(y => y.Mappings).Map(x => x.MapKey("ShapeElementId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}