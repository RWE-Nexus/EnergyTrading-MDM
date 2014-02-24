namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ProductTenorType" />
    /// </summary>
    public class ProductTenorTypeMapping : EntityMapping
    {
        public virtual ProductTenorType ProductTenorType { get; set; }

        protected override IEntity Entity
        {
            get { return this.ProductTenorType; }
            set { this.ProductTenorType = value as ProductTenorType; }
        }
    }
}