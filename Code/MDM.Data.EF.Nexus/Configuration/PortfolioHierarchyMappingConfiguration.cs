namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PortfolioHierarchyMappingConfiguration : EntityTypeConfiguration<PortfolioHierarchyMapping>
    {
        public PortfolioHierarchyMappingConfiguration()
        {
            this.ToTable("PortfolioHierarchyMapping");

            this.Property(x => x.Id).HasColumnName("PortfolioHierarchyMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.PortfolioHierarchy).WithMany(y => y.Mappings).Map(x => x.MapKey("PortfolioHierarchyId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}