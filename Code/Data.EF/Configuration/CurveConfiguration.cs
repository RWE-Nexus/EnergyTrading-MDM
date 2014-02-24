namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class CurveConfiguration : EntityTypeConfiguration<Curve>
    {
        public CurveConfiguration()
        {
            this.ToTable("Curve");
            this.Property(x => x.Id).HasColumnName("CurveId");

            this.Property(x => x.Name);
            this.Property(x => x.Type);
            this.Property(x => x.Currency).IsOptional();
            this.HasOptional(x => x.Commodity).WithMany().Map(s => s.MapKey("CommodityId"));
            this.Property(x => x.CommodityUnit).IsOptional();
            this.HasOptional(x => x.Location).WithMany().Map(s => s.MapKey("LocationId"));
            this.HasOptional(x => x.Originator).WithMany().Map(s => s.MapKey("OriginatorId"));
            this.Property(x => x.DefaultSpread).HasPrecision(18, 5);
            
            this.HasMany(x => x.Mappings);

            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}