namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class ShipperCodeMappingConfiguration : EntityTypeConfiguration<ShipperCodeMapping>
    {
        public ShipperCodeMappingConfiguration()
        {
            this.ToTable("ShipperCodeMapping");

            this.Property(x => x.Id).HasColumnName("ShipperCodeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.ShipperCode).WithMany(y => y.Mappings).Map(x => x.MapKey("ShipperCodeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}