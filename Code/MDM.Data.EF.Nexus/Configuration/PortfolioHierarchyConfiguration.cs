namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PortfolioHierarchyConfiguration : EntityTypeConfiguration<PortfolioHierarchy>
    {
        public PortfolioHierarchyConfiguration()
        {
            this.ToTable("PortfolioHierarchy");
            this.Property(x => x.Id).HasColumnName("PortfolioHierarchyId");

            this.HasRequired(x => x.Hierarachy).WithMany().Map(s => s.MapKey("HierarchyId"));
            this.HasOptional(x => x.ParentPortfolio).WithMany().Map(s => s.MapKey("ParentPortfolioId"));
            this.HasRequired(x => x.ChildPortfolio).WithMany().Map(s => s.MapKey("ChildPortfolioId"));

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}