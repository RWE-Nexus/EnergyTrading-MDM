namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PartyOverrideMappingConfiguration : EntityTypeConfiguration<PartyOverrideMapping>
    {
        public PartyOverrideMappingConfiguration()
        {
            this.ToTable("PartyOverrideMapping");

            this.Property(x => x.Id).HasColumnName("PartyOverrideMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.PartyOverride).WithMany(y => y.Mappings).Map(x => x.MapKey("PartyOverrideId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}