namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class LocationRoleMappingConfiguration : EntityTypeConfiguration<LocationRoleMapping>
    {
        public LocationRoleMappingConfiguration()
        {
            this.ToTable("LocationRoleMapping");

            this.Property(x => x.Id).HasColumnName("LocationRoleMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.LocationRole).WithMany(y => y.Mappings).Map(x => x.MapKey("LocationRoleId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}