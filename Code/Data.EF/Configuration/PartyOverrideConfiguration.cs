namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class PartyOverrideConfiguration : EntityTypeConfiguration<PartyOverride>
    {
        public PartyOverrideConfiguration()
        {
            this.ToTable("PartyOverride");
            this.Property(x => x.Id).HasColumnName("PartyOverrideId");

            this.HasOptional(x => x.Broker).WithMany().Map(s => s.MapKey("BrokerId"));
            this.HasRequired(x => x.CommodityInstrumentType).WithMany().Map(s => s.MapKey("CommodityInstrumentTypeId"));
            this.Property(x => x.MappingValue);
            this.HasRequired(x => x.Party).WithMany().Map(s => s.MapKey("PartyId"));

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}