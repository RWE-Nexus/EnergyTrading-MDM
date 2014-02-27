namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class BrokerConfiguration : EntityTypeConfiguration<Broker>
    {
        public BrokerConfiguration()
        {
            //this.ToTable("Broker");
            //this.Property(x => x.Id).HasColumnName("BrokerId");
            //this.HasMany(x => x.Details);
            //this.HasMany(x => x.Mappings);
            //this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}
