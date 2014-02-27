namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class PartyAccountabilityConfiguration : EntityTypeConfiguration<MDM.PartyAccountability>
    {
        public PartyAccountabilityConfiguration()
        {
            this.Map<PartyAccountability>(
                y =>
                {
                    y.Requires("PartyAccountabilityClass").HasValue("PartyAccountability").IsRequired();
                    y.ToTable("PartyAccountability");
                });

            this.Property(x => x.Id).HasColumnName("PartyAccountabilityId");
            this.Property(x => x.PartyAccountabilityType);
            this.HasOptional(x => x.SourceParty).WithMany().Map(x => x.MapKey("SourcePartyId"));
            this.HasOptional(x => x.TargetParty).WithMany().Map(x => x.MapKey("TargetPartyId"));
            this.HasOptional(x => x.SourcePerson).WithMany().Map(x => x.MapKey("SourcePersonId"));
            this.HasOptional(x => x.TargetPerson).WithMany().Map(x => x.MapKey("TargetPersonId"));
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}