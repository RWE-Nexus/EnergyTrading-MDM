using System.Data.Entity.ModelConfiguration;

namespace EnergyTrading.Mdm.Data.EF.Configuration
{
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