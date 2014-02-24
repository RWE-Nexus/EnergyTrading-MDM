namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class PartyRoleConfiguration : EntityTypeConfiguration<PartyRole>
    {
        public PartyRoleConfiguration()
        {
            this.Map<PartyRole>(
                y =>
                {
                    y.Requires("PartyRoleClass").HasValue("PartyRole").IsRequired();
                    y.ToTable("PartyRole");
                });

            this.Map<Exchange>(
                y =>
                    {
                        y.MapInheritedProperties();
                        y.Requires("PartyRoleClass").HasValue("Exchange").IsRequired();
                        y.ToTable("PartyRole");
                });

            this.Map<Broker>(
                y =>
                    {
                    y.MapInheritedProperties();
                    y.Requires("PartyRoleClass").HasValue("Broker").IsRequired();
                    y.ToTable("PartyRole");
                });

            this.Map<Counterparty>(
                y =>
                    {
                    y.MapInheritedProperties();
                    y.Requires("PartyRoleClass").HasValue("Counterparty").IsRequired();
                    y.ToTable("PartyRole");
                });

            this.Map<BusinessUnit>(
                y =>
                {
                    y.MapInheritedProperties();
                    y.Requires("PartyRoleClass").HasValue("BusinessUnit").IsRequired();
                    y.ToTable("PartyRole");
                });

            this.Map<LegalEntity>(
                y =>
                {
                    y.MapInheritedProperties();
                    y.Requires("PartyRoleClass").HasValue("LegalEntity").IsRequired();
                    y.ToTable("PartyRole");
                });

            this.HasRequired(x => x.Party).WithMany(y => y.PartyRoles).Map(x => x.MapKey("PartyId"));
            this.Property(x => x.Id).HasColumnName("PartyRoleId");
            this.HasMany(x => x.Details);
            this.HasMany(x => x.Mappings);
            this.Property(x => x.PartyRoleType).IsRequired();
        }
    }
}