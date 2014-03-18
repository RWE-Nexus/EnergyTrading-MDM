namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="BrokerCommodity" />
    /// </summary>
    public class BrokerCommodityMapping : EntityMapping
    {
        public virtual BrokerCommodity BrokerCommodity { get; set; }

        protected override IEntity Entity
        {
            get { return this.BrokerCommodity; }
            set { this.BrokerCommodity = value as BrokerCommodity; }
        }
    }
}