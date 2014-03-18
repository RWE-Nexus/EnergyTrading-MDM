namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            this.ToTable("Person");
            this.Property(x => x.Id).HasColumnName("PersonId");
            this.HasMany(x => x.Details).WithRequired().Map(map => map.MapKey("PersonId"));
            this.HasMany(x => x.Mappings);
        }
    }
}
