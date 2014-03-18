namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class InstrumentTypeMappingConfiguration : EntityTypeConfiguration<InstrumentTypeMapping>
    {
        public InstrumentTypeMappingConfiguration()
        {
            this.ToTable("InstrumentTypeMapping");

            this.Property(x => x.Id).HasColumnName("InstrumentTypeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.InstrumentType).WithMany(y => y.Mappings).Map(x => x.MapKey("InstrumentTypeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}