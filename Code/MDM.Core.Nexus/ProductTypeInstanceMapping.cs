namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ProductTypeInstance" />
    /// </summary>
    public class ProductTypeInstanceMapping : EntityMapping
    {
        public virtual ProductTypeInstance ProductTypeInstance { get; set; }

        protected override IEntity Entity
        {
            get { return this.ProductTypeInstance; }
            set { this.ProductTypeInstance = value as ProductTypeInstance; }
        }
    }
}