namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="CommodityInstrumentType" />
    /// </summary>
    public class CommodityInstrumentTypeMapping : EntityMapping
    {
        public virtual CommodityInstrumentType CommodityInstrumentType { get; set; }

        protected override IEntity Entity
        {
            get { return this.CommodityInstrumentType; }
            set { this.CommodityInstrumentType = value as CommodityInstrumentType; }
        }
    }
}