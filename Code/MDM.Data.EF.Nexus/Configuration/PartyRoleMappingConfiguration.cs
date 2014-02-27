namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class PartyRoleMappingConfiguration : EntityTypeConfiguration<PartyRoleMapping>
    {
        public PartyRoleMappingConfiguration()
        {
            this.ToTable("PartyRoleMapping");

            this.Property(x => x.Id).HasColumnName("PartyRoleMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.PartyRole).WithMany(y => y.Mappings).Map(x => x.MapKey("PartyRoleId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}