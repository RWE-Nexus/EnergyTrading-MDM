namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class ShapeDayConfiguration : EntityTypeConfiguration<ShapeDay>
    {
        public ShapeDayConfiguration()
        {
            this.ToTable("ShapeDay");
           
            this.Property(x => x.Id).HasColumnName("ShapeDayId");
            
            this.Property(x => x.DayType);
            this.HasRequired(x => x.Shape).WithMany().Map(s => s.MapKey("ShapeId"));
            this.HasRequired(x => x.ShapeElement).WithMany().Map(s => s.MapKey("ShapeElementId"));
            
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}