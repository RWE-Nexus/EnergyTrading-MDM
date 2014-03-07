namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class MarketMappingConfiguration : EntityTypeConfiguration<MarketMapping>
    {
        public MarketMappingConfiguration()
        {
            this.ToTable("MarketMapping");

            this.Property(x => x.Id).HasColumnName("MarketMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Market).WithMany(y => y.Mappings).Map(x => x.MapKey("MarketId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}