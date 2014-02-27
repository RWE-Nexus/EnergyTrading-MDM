namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ProductType" />
    /// </summary>
    public class ProductTypeMapping : EntityMapping
    {
        public virtual ProductType ProductType { get; set; }

        protected override IEntity Entity
        {
            get { return this.ProductType; }
            set { this.ProductType = value as ProductType; }
        }
    }
}