namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class CommodityFeeTypeMappingConfiguration : EntityTypeConfiguration<CommodityFeeTypeMapping>
    {
        public CommodityFeeTypeMappingConfiguration()
        {
            this.ToTable("CommodityFeeTypeMapping");

            this.Property(x => x.Id).HasColumnName("CommodityFeeTypeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.CommodityFeeType).WithMany(y => y.Mappings).Map(x => x.MapKey("CommodityFeeTypeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}