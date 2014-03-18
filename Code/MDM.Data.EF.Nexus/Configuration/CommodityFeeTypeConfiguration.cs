namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    

    class CommodityFeeTypeConfiguration : EntityTypeConfiguration<CommodityFeeType>
    {
        public CommodityFeeTypeConfiguration()
        {
            this.ToTable("CommodityFeeType");
            this.Property(x => x.Id).HasColumnName("CommodityFeeTypeId");
            this.HasRequired(x => x.Commodity).WithMany().Map(s => s.MapKey("CommodityId"));
            this.HasOptional(x => x.FeeType).WithMany().Map(s => s.MapKey("FeeTypeId"));
			// TODO_CodeGeneration_CommodityFeeType - add properties that should be mapped to the database
            //this.Property(x => x.Name);
            this.HasMany(x => x.Mappings);
            this.Property(x => x.Validity.Start).HasColumnName("Start");
            this.Property(x => x.Validity.Finish).HasColumnName("Finish");			
            this.Property(x => x.Timestamp).IsRowVersion().HasColumnName("Version");
           
        }
    }
}