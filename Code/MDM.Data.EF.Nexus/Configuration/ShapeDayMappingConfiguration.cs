namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class ShapeDayMappingConfiguration : EntityTypeConfiguration<ShapeDayMapping>
    {
        public ShapeDayMappingConfiguration()
        {
            this.ToTable("ShapeDayMapping");

            this.Property(x => x.Id).HasColumnName("ShapeDayMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ShapeDay).WithMany(y => y.Mappings).Map(x => x.MapKey("ShapeDayId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}