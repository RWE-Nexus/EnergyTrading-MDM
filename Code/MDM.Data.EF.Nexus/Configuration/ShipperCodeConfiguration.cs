namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    public class ShipperCodeConfiguration : EntityTypeConfiguration<ShipperCode>
    {
        public ShipperCodeConfiguration()
        {
            this.ToTable("ShipperCode");
            this.Property(x => x.Id).HasColumnName("ShipperCodeId");
            //this.HasMany(x => x.Details);
            this.HasRequired(x => x.Location).WithMany().Map(s => s.MapKey("LocationId"));
            this.HasRequired(x => x.Party).WithMany().Map(s => s.MapKey("PartyId"));
            this.Property(x => x.Code).HasColumnName("ShipperCode");
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
        }
    }
}
