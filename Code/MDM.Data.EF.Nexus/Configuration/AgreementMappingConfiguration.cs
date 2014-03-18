namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class AgreementMappingConfiguration : EntityTypeConfiguration<AgreementMapping>
    {
        public AgreementMappingConfiguration()
        {
            this.ToTable("AgreementMapping");
            this.Property(x => x.Id).HasColumnName("AgreementMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Agreement).WithMany(y => y.Mappings).Map(x => x.MapKey("AgreementId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}