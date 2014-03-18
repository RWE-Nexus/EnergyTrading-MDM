namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    public class CalendarDayConfiguration : EntityTypeConfiguration<CalendarDay>
    {
        public CalendarDayConfiguration()
        {
            this.ToTable("CalendarDay");
            this.Property(x => x.Id).HasColumnName("CalendarDayId");
            this.HasRequired(x => x.Calendar).WithMany(y => y.Days).Map(x => x.MapKey("CalendarId"));
            this.Property(x => x.Date).HasColumnName("CalendarDate");
            this.Property(x => x.DayType);
        } 
    }
}