namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PortfolioConfiguration : EntityTypeConfiguration<Portfolio>
    {
        public PortfolioConfiguration()
        {
            this.ToTable("Portfolio");
            this.Property(x => x.Id).HasColumnName("PortfolioId");
            // this.HasMany(x => x.Details);
            this.Property(x => x.Name);
            this.Property(x => x.PortfolioType);
            this.HasOptional(x => x.BusinessUnit).WithMany().Map(x => x.MapKey("BusinessUnitId"));
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}