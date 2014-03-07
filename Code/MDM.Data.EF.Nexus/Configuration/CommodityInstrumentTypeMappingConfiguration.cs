namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class CommodityInstrumentTypeMappingConfiguration : EntityTypeConfiguration<CommodityInstrumentTypeMapping>
    {
        public CommodityInstrumentTypeMappingConfiguration()
        {
            this.ToTable("CommodityInstrumentTypeMapping");

            this.Property(x => x.Id).HasColumnName("CommodityInstrumentTypeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.CommodityInstrumentType).WithMany(y => y.Mappings).Map(x => x.MapKey("CommodityInstrumentTypeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}