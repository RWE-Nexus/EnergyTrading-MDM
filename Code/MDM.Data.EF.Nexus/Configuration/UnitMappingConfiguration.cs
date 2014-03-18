namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class UnitMappingConfiguration : EntityTypeConfiguration<UnitMapping>
    {
        public UnitMappingConfiguration()
        {
            this.ToTable("UnitMapping");

            this.Property(x => x.Id).HasColumnName("UnitMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Unit).WithMany(y => y.Mappings).Map(x => x.MapKey("UnitId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}