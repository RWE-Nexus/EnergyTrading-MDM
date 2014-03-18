namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class LocationRoleConfiguration : EntityTypeConfiguration<LocationRole>
    {
        public LocationRoleConfiguration()
        {
            this.ToTable("LocationRole");
         
            this.Property(x => x.Id).HasColumnName("LocationRoleId");
            this.HasRequired(x => x.Type).WithMany().Map(x => x.MapKey("LocationRoleTypeId")); 
            this.HasRequired(x => x.Location).WithMany().Map(x => x.MapKey("LocationId")); 
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}
