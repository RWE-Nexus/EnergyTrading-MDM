namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PartyConfiguration : EntityTypeConfiguration<Party>
    {
        public PartyConfiguration()
        {
            this.ToTable("Party");
            this.Property(x => x.Id).HasColumnName("PartyId");
            this.HasMany(x => x.PartyRoles).WithRequired().Map(x => x.MapKey("PartyId"));
            this.HasMany(x => x.Details).WithRequired().Map(map => map.MapKey("PartyId"));
            this.HasMany(x => x.Details);
            this.HasMany(x => x.Mappings);
        }
    }
}

