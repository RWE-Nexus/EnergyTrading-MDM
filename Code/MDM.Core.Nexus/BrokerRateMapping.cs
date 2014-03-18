namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="BrokerRate" />
    /// </summary>
    public class BrokerRateMapping : EntityMapping
    {
        public virtual BrokerRate BrokerRate { get; set; }

        protected override IEntity Entity
        {
            get { return this.BrokerRate; }
            set { this.BrokerRate = value as BrokerRate; }
        }
    }
}