namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    public class CalendarConfiguration : EntityTypeConfiguration<Calendar>
    {
        public CalendarConfiguration()
        {
            this.ToTable("Calendar");
            this.Property(x => x.Id).HasColumnName("CalendarId");
            this.Property(x => x.Name);
            this.HasMany(x => x.Days).WithRequired().Map(x => x.MapKey("CalendarId"));
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}
