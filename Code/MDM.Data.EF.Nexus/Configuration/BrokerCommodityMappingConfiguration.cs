namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class BrokerCommodityMappingConfiguration : EntityTypeConfiguration<BrokerCommodityMapping>
    {
        public BrokerCommodityMappingConfiguration()
        {
            this.ToTable("BrokerCommodityMapping");

            this.Property(x => x.Id).HasColumnName("BrokerCommodityMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.BrokerCommodity).WithMany(y => y.Mappings).Map(x => x.MapKey("BrokerCommodityId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}