namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class AgreementConfiguration : EntityTypeConfiguration<Agreement>
    {
        public AgreementConfiguration()
        {
            this.ToTable("Agreement");
            this.Property(x => x.Id).HasColumnName("AgreementId");
            this.Property(x => x.Name);
            this.Property(x => x.PaymentTerms);
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}