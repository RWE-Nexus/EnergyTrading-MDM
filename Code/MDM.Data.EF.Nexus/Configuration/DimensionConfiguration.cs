namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class DimensionConfiguration : EntityTypeConfiguration<Dimension>
    {
        public DimensionConfiguration()
        {
            this.ToTable("Dimension");
            this.Property(x => x.Id).HasColumnName("DimensionId");

            this.Property(x => x.Name);
            this.Property(x => x.Description);
            this.Property(x => x.LengthDimension);
            this.Property(x => x.MassDimension);
            this.Property(x => x.TimeDimension);
            this.Property(x => x.ElectricCurrentDimension);
            this.Property(x => x.TemperatureDimension);
            this.Property(x => x.LuminousIntensityDimension);

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}