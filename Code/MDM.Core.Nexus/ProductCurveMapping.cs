namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ProductCurve" />
    /// </summary>
    public class ProductCurveMapping : EntityMapping
    {
        public virtual ProductCurve ProductCurve { get; set; }

        protected override IEntity Entity
        {
            get { return this.ProductCurve; }
            set { this.ProductCurve = value as ProductCurve; }
        }
    }
}