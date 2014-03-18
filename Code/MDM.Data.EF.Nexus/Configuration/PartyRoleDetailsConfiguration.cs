namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PartyRoleDetailsConfiguration: EntityTypeConfiguration<PartyRoleDetails>
    {
        public PartyRoleDetailsConfiguration()
        {
            this.Map<PartyRoleDetails>(
                y =>
                {
                    y.Requires("PartyRoleDetailsClass").HasValue("PartyRoleDetails").IsRequired();
                    y.ToTable("PartyRoleDetails");
                });

            this.Property(x => x.Id).HasColumnName("PartyRoleDetailsId");
            this.HasRequired(x => x.PartyRole).WithMany(y => y.Details).Map(x => x.MapKey("PartyRoleId")); 
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}