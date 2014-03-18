namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PersonMappingConfiguration : EntityTypeConfiguration<PersonMapping>
    {
        public PersonMappingConfiguration()
        {
            this.ToTable("PersonMapping");

            this.Property(x => x.Id).HasColumnName("PersonMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Person).WithMany(y => y.Mappings).Map(x => x.MapKey("PersonId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}