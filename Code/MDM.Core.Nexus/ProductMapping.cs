namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Product" />
    /// </summary>
    public class ProductMapping : EntityMapping
    {
        public virtual Product Product { get; set; }

        protected override IEntity Entity
        {
            get { return this.Product; }
            set { this.Product = value as Product; }
        }
    }
}