namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class PartyRoleAccountabilityMappingConfiguration : EntityTypeConfiguration<PartyRoleAccountabilityMapping>
    {
        public PartyRoleAccountabilityMappingConfiguration()
        {
            this.ToTable("PartyRoleAccountabilityMapping");

            this.Property(x => x.Id).HasColumnName("PartyRoleAccountabilityMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.PartyRoleAccountability).WithMany(y => y.Mappings).Map(x => x.MapKey("PartyRoleAccountabilityId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}