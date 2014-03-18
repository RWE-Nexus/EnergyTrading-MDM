namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class FeeTypeMappingConfiguration : EntityTypeConfiguration<FeeTypeMapping>
    {
        public FeeTypeMappingConfiguration()
        {
            this.ToTable("FeeTypeMapping");

            this.Property(x => x.Id).HasColumnName("FeeTypeMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.FeeType).WithMany(y => y.Mappings).Map(x => x.MapKey("FeeTypeId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}