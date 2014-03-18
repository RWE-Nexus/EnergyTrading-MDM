namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PersonDetailsConfiguration : EntityTypeConfiguration<PersonDetails>
    {
        public PersonDetailsConfiguration()
        {
            this.Property(x => x.Id).HasColumnName("PersonDetailsId");
            this.HasRequired(x => x.Person).WithMany(y => y.Details).Map(x => x.MapKey("PersonId"));
            this.Property(x => x.FirstName);
            this.Property(x => x.LastName);
            this.Property(x => x.Phone);
            this.Property(x => x.Fax);
            this.Property(x => x.Role);
            this.Property(x => x.Email);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}