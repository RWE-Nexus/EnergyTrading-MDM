namespace RWEST.Nexus.MDM.Data.EF.Configuration
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class ExchangeConfiguration : EntityTypeConfiguration<Exchange>
    {
        public ExchangeConfiguration()
        {


			// TODO_CodeGeneration_Exchange - add properties that should be mapped to the database
            // this.Property(x => x.Name);

            // this.HasMany(x => x.Mappings);
            // this.Property(x => x.Validity.Start).HasColumnName("Start");
            // this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            // this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}