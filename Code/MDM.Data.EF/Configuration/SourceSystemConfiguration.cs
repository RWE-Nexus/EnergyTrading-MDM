namespace EnergyTrading.Mdm.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using EnergyTrading.Mdm;

    public class SourceSystemConfiguration : EntityTypeConfiguration<SourceSystem>
    {
        public SourceSystemConfiguration()
        {
            this.ToTable("SourceSystem");
            this.Property(x => x.Id).HasColumnName("SourceSystemId");
            this.HasOptional(x => x.Parent).WithMany().Map(x => x.MapKey("ParentSourceSystemId"));
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Name).HasColumnName("SourceSystemName");
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}