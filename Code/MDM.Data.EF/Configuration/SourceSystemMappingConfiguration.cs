namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using EnergyTrading.Mdm;

    public class SourceSystemMappingConfiguration : EntityTypeConfiguration<SourceSystemMapping>
    {
        public SourceSystemMappingConfiguration()
        {
            this.ToTable("SourceSystemMapping");

            this.Property(x => x.Id).HasColumnName("SourceSystemMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SystemId"));
            this.HasRequired(x => x.SourceSystem).WithMany(y => y.Mappings).Map(x => x.MapKey("SourceSystemId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}