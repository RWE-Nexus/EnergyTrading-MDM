namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using RWEST.Nexus.MDM;

    class FeeTypeConfiguration : EntityTypeConfiguration<FeeType>
    {
        public FeeTypeConfiguration()
        {
            this.ToTable("FeeType");
            this.Property(x => x.Id).HasColumnName("FeeTypeId");

			// TODO_CodeGeneration_FeeType - add properties that should be mapped to the database
            // this.Property(x => x.Name);

            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}