namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class CurveMappingConfiguration : EntityTypeConfiguration<CurveMapping>
    {
        public CurveMappingConfiguration()
        {
            this.ToTable("CurveMapping");

            this.Property(x => x.Id).HasColumnName("CurveMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Curve).WithMany(y => y.Mappings).Map(x => x.MapKey("CurveId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}