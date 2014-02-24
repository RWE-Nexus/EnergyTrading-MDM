namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="CommodityFeeType" />
    /// </summary>
    public class CommodityFeeTypeMapping : EntityMapping
    {
        public virtual CommodityFeeType CommodityFeeType { get; set; }

        protected override IEntity Entity
        {
            get { return this.CommodityFeeType; }
            set { this.CommodityFeeType = value as CommodityFeeType; }
        }
    }
}