namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class PartyAccountabilityDetailsConfiguration: EntityTypeConfiguration<PartyAccountabilityDetails>
    {
        public PartyAccountabilityDetailsConfiguration()
        {
            this.Map<PartyAccountabilityDetails>(
                y =>
                {
                    y.Requires("PartyAccountabilityDetailsClass").HasValue("PartyAccountabilityDetails").IsRequired();
                    y.ToTable("PartyAccountabilityDetails");
                });

            this.Property(x => x.Id).HasColumnName("PartyAccountabilityDetailsId");
            this.HasRequired(x => x.PartyAccountability).WithMany(y => y.Details).Map(x => x.MapKey("PartyAccountabilityId")); 
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}