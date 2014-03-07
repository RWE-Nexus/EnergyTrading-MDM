namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Market" />
    /// </summary>
    public class MarketMapping : EntityMapping
    {
        public virtual Market Market { get; set; }

        protected override IEntity Entity
        {
            get { return this.Market; }
            set { this.Market = value as Market; }
        }
    }
}