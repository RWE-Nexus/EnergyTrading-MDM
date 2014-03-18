namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ProductScota" />
    /// </summary>
    public class ProductScotaMapping : EntityMapping
    {
        public virtual ProductScota ProductScota { get; set; }

        protected override IEntity Entity
        {
            get { return this.ProductScota; }
            set { this.ProductScota = value as ProductScota; }
        }
    }
}