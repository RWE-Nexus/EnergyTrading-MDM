namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class PartyMappingConfiguration : EntityTypeConfiguration<PartyMapping>
    {
        public PartyMappingConfiguration()
        {
            this.ToTable("PartyMapping");

            this.Property(x => x.Id).HasColumnName("PartyMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Party).WithMany(y => y.Mappings).Map(x => x.MapKey("PartyId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}
