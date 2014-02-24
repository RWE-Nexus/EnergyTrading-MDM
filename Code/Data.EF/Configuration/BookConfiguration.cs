namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            this.ToTable("Book");
            this.Property(x => x.Id).HasColumnName("BookId");

            this.Property(x => x.Name);

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}