namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class PartyAccountabilityMappingConfiguration : EntityTypeConfiguration<PartyAccountabilityMapping>
    {
        public PartyAccountabilityMappingConfiguration()
        {
            this.ToTable("PartyAccountabilityMapping");

            this.Property(x => x.Id).HasColumnName("PartyAccountabilityMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.PartyAccountability).WithMany(y => y.Mappings).Map(x => x.MapKey("PartyAccountabilityId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}