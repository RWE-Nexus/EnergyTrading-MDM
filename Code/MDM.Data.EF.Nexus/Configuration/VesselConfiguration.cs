namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class VesselConfiguration : EntityTypeConfiguration<Vessel>
    {
        public VesselConfiguration()
        {
            this.ToTable("Vessel");
            this.Property(x => x.Id).HasColumnName("VesselId");

			this.Property(x => x.Name);

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}