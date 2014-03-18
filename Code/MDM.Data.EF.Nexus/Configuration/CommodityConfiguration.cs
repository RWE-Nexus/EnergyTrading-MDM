namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class CommodityConfiguration : EntityTypeConfiguration<Commodity>
    {
        public CommodityConfiguration()
        {
            this.ToTable("Commodity");
            this.Property(x => x.Id).HasColumnName("CommodityId");
            this.Property(x => x.Name);
            this.Property(x => x.MassEnergyValue).HasPrecision(18, 10).IsOptional();
            this.Property(x => x.VolumeEnergyValue).HasPrecision(18, 10).IsOptional();
            this.Property(x => x.VolumetricDensityValue).HasPrecision(18, 10).IsOptional();
            this.Property(x => x.DeliveryRate);
            this.HasOptional(x => x.Parent).WithMany().Map(x => x.MapKey("ParentCommodityId"));
            this.HasOptional(x => x.MassEnergyUnits).WithMany().Map(s => s.MapKey("MassEnergyUnitId"));
            this.HasOptional(x => x.VolumeEnergyUnits).WithMany().Map(s => s.MapKey("VolumeEnergyUnitId"));
            this.HasOptional(x => x.VolumetricDensityUnits).WithMany().Map(s => s.MapKey("VolumetricDensityUnitId"));
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}