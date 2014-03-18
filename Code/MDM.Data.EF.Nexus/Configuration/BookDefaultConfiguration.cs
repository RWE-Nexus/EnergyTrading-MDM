namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class BookDefaultConfiguration : EntityTypeConfiguration<BookDefault>
    {
        public BookDefaultConfiguration()
        {
            this.ToTable("BookDefault");
            this.Property(x => x.Id).HasColumnName("BookDefaultId");

            this.Property(x => x.Name);
            this.Property(x => x.GfProductMapping);
            this.HasOptional(x => x.Trader).WithMany().Map(s => s.MapKey("TraderId"));
            this.HasOptional(x => x.Desk).WithMany().Map(s => s.MapKey("DeskId"));
            this.HasRequired(x => x.Book).WithMany().Map(s => s.MapKey("BookId"));
            this.Property(x => x.DefaultType);
            this.HasOptional(x => x.PartyRole).WithMany().Map(s => s.MapKey("RoleId"));

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}