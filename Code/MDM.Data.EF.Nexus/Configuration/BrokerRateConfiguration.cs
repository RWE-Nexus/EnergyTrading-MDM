namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class BrokerRateConfiguration : EntityTypeConfiguration<BrokerRate>
    {
        public BrokerRateConfiguration()
        {
            this.ToTable("BrokerRate");
            this.Property(x => x.Id).HasColumnName("BrokerRateId");
            this.HasMany(x => x.Details).WithRequired().Map(map => map.MapKey("BrokerRateId"));
            this.HasMany(x => x.Details);
            this.HasMany(x => x.Mappings);
         }
    }
}