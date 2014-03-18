namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class BookDefaultMappingConfiguration : EntityTypeConfiguration<BookDefaultMapping>
    {
        public BookDefaultMappingConfiguration()
        {
            this.ToTable("BookDefaultMapping");

            this.Property(x => x.Id).HasColumnName("BookDefaultMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.BookDefault).WithMany(y => y.Mappings).Map(x => x.MapKey("BookDefaultId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}