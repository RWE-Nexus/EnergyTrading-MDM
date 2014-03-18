namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class LocationRoleTypeConfiguration : EntityTypeConfiguration<LocationRoleType>
    {
        public LocationRoleTypeConfiguration()
        {
            this.ToTable("LocationRoleType");
            this.Property(x => x.Id).HasColumnName("LocationRoleTypeId");
            this.Property(x => x.Name);
        }
    }
}