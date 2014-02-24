namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ReferenceDataConfiguration : EntityTypeConfiguration<ReferenceData>
    {
        public ReferenceDataConfiguration()
        {
            this.ToTable("ReferenceData");
            this.Property(x => x.Id).HasColumnName("ReferenceDataId");
            this.Property(x => x.Key).HasColumnName("ReferenceKey");
            this.Property(x => x.Value).HasColumnName("Value");
        }
    }
}