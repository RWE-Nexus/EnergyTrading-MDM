namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class ShapeConfiguration : EntityTypeConfiguration<Shape>
    {
        public ShapeConfiguration()
        {
            this.ToTable("Shape");

            this.Property(x => x.Id).HasColumnName("ShapeId");
            this.Property(x => x.Name);

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}