namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class ShapeElementConfiguration : EntityTypeConfiguration<ShapeElement>
    {
        public ShapeElementConfiguration()
        {
            this.ToTable("ShapeElement");
            
            this.Property(x => x.Id).HasColumnName("ShapeElementId");
            
            this.Property(x => x.Name);
            this.Property(x => x.Period.Start).HasColumnName("PeriodStart");
            this.Property(x => x.Period.Finish).HasColumnName("PeriodFinish");
            
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}