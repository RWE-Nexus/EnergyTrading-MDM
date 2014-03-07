namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="PortfolioHierarchy" />
    /// </summary>
    public class PortfolioHierarchyMapping : EntityMapping
    {
        public virtual PortfolioHierarchy PortfolioHierarchy { get; set; }

        protected override IEntity Entity
        {
            get { return this.PortfolioHierarchy; }
            set { this.PortfolioHierarchy = value as PortfolioHierarchy; }
        }
    }
}