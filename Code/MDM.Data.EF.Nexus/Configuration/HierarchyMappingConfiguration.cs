namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class HierarchyMappingConfiguration : EntityTypeConfiguration<HierarchyMapping>
    {
        public HierarchyMappingConfiguration()
        {
            this.ToTable("HierarchyMapping");

            this.Property(x => x.Id).HasColumnName("HierarchyMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Hierarchy).WithMany(y => y.Mappings).Map(x => x.MapKey("HierarchyId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}