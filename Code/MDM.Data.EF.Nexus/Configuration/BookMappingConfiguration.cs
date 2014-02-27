namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class BookMappingConfiguration : EntityTypeConfiguration<BookMapping>
    {
        public BookMappingConfiguration()
        {
            this.ToTable("BookMapping");

            this.Property(x => x.Id).HasColumnName("BookMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Book).WithMany(y => y.Mappings).Map(x => x.MapKey("BookId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}