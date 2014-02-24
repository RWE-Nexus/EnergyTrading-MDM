namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class CounterpartyConfiguration : EntityTypeConfiguration<Counterparty>
    {
        public CounterpartyConfiguration()
        {
            //this.ToTable("Counterparty");
            //this.Property(x => x.Id).HasColumnName("CounterpartyId");
            ////this.HasMany(x => x.Details);
            //this.Property(x => x.Name);
            //this.HasMany(x => x.Mappings);
            //this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}
