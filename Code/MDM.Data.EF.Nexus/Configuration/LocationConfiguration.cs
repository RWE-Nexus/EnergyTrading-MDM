namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            this.ToTable("Location");
            this.Property(x => x.Id).HasColumnName("LocationId");
            this.Property(x => x.Type).HasColumnName("Type");
            this.HasOptional(x => x.Parent).WithMany().Map(x => x.MapKey("ParentLocationId"));
            //this.HasMany(x => x.Details);
            this.Property(x => x.Name);
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}