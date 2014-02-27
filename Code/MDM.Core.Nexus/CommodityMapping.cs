namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Commodity" />
    /// </summary>
    public class CommodityMapping : EntityMapping
    {
        public virtual Commodity Commodity { get; set; }

        protected override IEntity Entity
        {
            get { return this.Commodity; }
            set { this.Commodity = value as Commodity; }
        }
    }
}