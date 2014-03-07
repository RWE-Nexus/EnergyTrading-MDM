namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class TenorMappingConfiguration : EntityTypeConfiguration<TenorMapping>
    {
        public TenorMappingConfiguration()
        {
            this.ToTable("TenorMapping");

            this.Property(x => x.Id).HasColumnName("TenorMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Tenor).WithMany(y => y.Mappings).Map(x => x.MapKey("TenorId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}