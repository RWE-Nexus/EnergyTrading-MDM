namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class CalendarMappingConfiguration : EntityTypeConfiguration<CalendarMapping>
    {
        public CalendarMappingConfiguration()
        {
            this.ToTable("CalendarMapping");

            this.Property(x => x.Id).HasColumnName("CalendarMappingId");
            this.HasRequired(x => x.System).WithMany().Map(x => x.MapKey("SourceSystemId"));
            this.HasRequired(x => x.Calendar).WithMany(y => y.Mappings).Map(x => x.MapKey("CalendarId"));
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Version).IsRowVersion();
        }
    }
}