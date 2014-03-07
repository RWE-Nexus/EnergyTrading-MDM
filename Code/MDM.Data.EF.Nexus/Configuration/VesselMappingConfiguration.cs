namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class VesselMappingConfiguration : EntityTypeConfiguration<VesselMapping>
    {
        public VesselMappingConfiguration()
        {
            this.ToTable("VesselMapping");

            this.Property(x => x.Id).HasColumnName("VesselMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Vessel).WithMany(y => y.Mappings).Map(x => x.MapKey("VesselId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}