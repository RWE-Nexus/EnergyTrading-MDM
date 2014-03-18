namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class InstrumentTypeOverrideMappingConfiguration : EntityTypeConfiguration<InstrumentTypeOverrideMapping>
    {
        public InstrumentTypeOverrideMappingConfiguration()
        {
            this.ToTable("InstrumentTypeOverrideMapping");

            this.Property(x => x.Id).HasColumnName("InstrumentTypeOverrideMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.InstrumentTypeOverride).WithMany(y => y.Mappings).Map(x => x.MapKey("InstrumentTypeOverrideId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}