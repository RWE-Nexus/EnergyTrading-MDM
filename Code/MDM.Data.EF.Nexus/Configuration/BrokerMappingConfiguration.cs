namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class BrokerMappingConfiguration : EntityTypeConfiguration<BrokerMapping>
    {
        //public BrokerMappingConfiguration()
        //{
        //    this.ToTable("BrokerMapping");

        //    this.Property(x => x.Id).HasColumnName("BrokerMappingId");
        //    this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
        //    this.HasRequired(x => x.Broker).WithMany(y => y.Mappings).Map(x => x.MapKey("BrokerId"));
        //    this.Property(x => x.Validity.Start).HasColumnName("Start");
        //    this.Property(x => x.Validity.Finish).HasColumnName("Finish");
        //    this.Property(x => x.Version).IsRowVersion();
        //}
    }
}