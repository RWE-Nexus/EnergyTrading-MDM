namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class CounterpartyMappingConfiguration : EntityTypeConfiguration<CounterpartyMapping>
    {
        public CounterpartyMappingConfiguration()
        {
            //this.ToTable("CounterpartyMapping");

            //this.Property(x => x.Id).HasColumnName("CounterpartyMappingId");
            //this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            //this.HasRequired(x => x.Counterparty).WithMany(y => y.Mappings).Map(x => x.MapKey("CounterpartyId"));
            //this.Property(x => x.Validity.Start).HasColumnName("Start");
            //this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            //this.Property(x => x.Version).IsRowVersion();
        }
    }
}