namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class HierarchyConfiguration : EntityTypeConfiguration<Hierarchy>
    {
        public HierarchyConfiguration()
        {
            this.ToTable("Hierarchy");
            this.Property(x => x.Id).HasColumnName("HierarchyId");
            this.Property(x => x.Name);

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}