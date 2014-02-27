namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class BrokerRateMappingConfiguration : EntityTypeConfiguration<BrokerRateMapping>
    {
        public BrokerRateMappingConfiguration()
        {
            this.ToTable("BrokerRateMapping");

            this.Property(x => x.Id).HasColumnName("BrokerRateMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.BrokerRate).WithMany(y => y.Mappings).Map(x => x.MapKey("BrokerRateId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}