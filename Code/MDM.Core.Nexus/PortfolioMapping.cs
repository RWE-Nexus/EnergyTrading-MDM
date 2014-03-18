namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Portfolio" />
    /// </summary>
    public class PortfolioMapping : EntityMapping
    {
        public virtual Portfolio Portfolio { get; set; }

        protected override IEntity Entity
        {
            get { return this.Portfolio; }
            set { this.Portfolio = value as Portfolio; }
        }
    }
}