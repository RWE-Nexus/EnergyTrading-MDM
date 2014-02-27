namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class ExchangeMappingConfiguration : EntityTypeConfiguration<ExchangeMapping>
    {
        public ExchangeMappingConfiguration()
        {
            //this.ToTable("ExchangeMapping");

            //this.Property(x => x.Id).HasColumnName("ExchangeMappingId");
            //this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            //this.HasRequired(x => x.Exchange).WithMany(y => y.Mappings).Map(x => x.MapKey("ExchangeId"));
            //this.Property(x => x.Validity.Start).HasColumnName("Start");
            //this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            //this.Property(x => x.Version).IsRowVersion();
        }
    }
}