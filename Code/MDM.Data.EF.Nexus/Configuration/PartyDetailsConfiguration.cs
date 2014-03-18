namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PartyDetailsConfiguration : EntityTypeConfiguration<PartyDetails>
    {
        public PartyDetailsConfiguration()
        {
            this.Property(x => x.Id).HasColumnName("PartyDetailsId");
            this.HasRequired(x => x.Party).WithMany(y => y.Details).Map(x => x.MapKey("PartyId"));
            this.Property(x => x.Name);
            this.Property(x => x.Phone);
            this.Property(x => x.Fax);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
            this.Property(x => x.IsInternal);
        }
    }
}