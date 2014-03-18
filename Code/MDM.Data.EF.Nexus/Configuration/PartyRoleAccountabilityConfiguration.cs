namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class PartyRoleAccountabilityConfiguration : EntityTypeConfiguration<PartyRoleAccountability>
    {
        public PartyRoleAccountabilityConfiguration()
        {
            this.Map<PartyRoleAccountability>(y => y.ToTable("PartyRoleAccountability"));

            this.Property(x => x.Id).HasColumnName("PartyRoleAccountabilityId");
            this.Property(x => x.PartyRoleAccountabilityType);
            this.HasOptional(x => x.SourcePartyRole).WithMany().Map(x => x.MapKey("SourcePartyRoleId"));
            this.HasOptional(x => x.TargetPartyRole).WithMany().Map(x => x.MapKey("TargetPartyRoleId"));
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}