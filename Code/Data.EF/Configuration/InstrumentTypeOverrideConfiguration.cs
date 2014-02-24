namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class InstrumentTypeOverrideConfiguration : EntityTypeConfiguration<InstrumentTypeOverride>
    {
        public InstrumentTypeOverrideConfiguration()
        {
            this.ToTable("InstrumentTypeOverride");
            this.Property(x => x.Id).HasColumnName("InstrumentTypeOverrideId");

            this.Property(x => x.Name);
            this.HasRequired(x => x.ProductType).WithMany().Map(s => s.MapKey("ProductTypeId"));
            this.HasRequired(x => x.Broker).WithMany().Map(s => s.MapKey("BrokerId"));
            this.HasRequired(x => x.CommodityInstrumentType).WithMany().Map(s => s.MapKey("CommodityInstrumentTypeId"));
            this.Property(x => x.InstrumentSubType);
            this.HasOptional(x => x.ProductTenorType).WithMany().Map(s => s.MapKey("ProductTenorTypeId"));

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}