namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class LocationMappingConfiguration : EntityTypeConfiguration<LocationMapping>
    {
        public LocationMappingConfiguration()
        {
            this.ToTable("LocationMapping");

            this.Property(x => x.Id).HasColumnName("LocationMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Location).WithMany(y => y.Mappings).Map(x => x.MapKey("LocationId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}