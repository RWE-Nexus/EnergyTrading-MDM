namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class UnitConfiguration : EntityTypeConfiguration<Unit>
    {
        public UnitConfiguration()
        {
            this.ToTable("Unit");
            this.Property(x => x.Id).HasColumnName("UnitId");

            this.Property(x => x.Name);
            this.Property(x => x.Description);
            this.Property(x => x.ConversionConstant).HasPrecision(18, 10);
            this.Property(x => x.ConversionFactor).HasPrecision(18, 10);
            this.Property(x => x.Symbol);
            this.HasOptional(x => x.Dimension).WithMany().Map(d => d.MapKey("DimensionId"));

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}