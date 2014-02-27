namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class BrokerCommodityConfiguration : EntityTypeConfiguration<BrokerCommodity>
    {
        public BrokerCommodityConfiguration()
        {
            this.ToTable("BrokerCommodity");
            this.Property(x => x.Id).HasColumnName("BrokerCommodityId");

			this.Property(x => x.Name);
            this.HasRequired(x => x.Broker).WithMany().Map(s => s.MapKey("BrokerId"));
            this.HasRequired(x => x.Commodity).WithMany().Map(s => s.MapKey("CommodityId"));

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}