namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="TenorType" />
    /// </summary>
    public class TenorTypeMapping : EntityMapping
    {
        public virtual TenorType TenorType { get; set; }

        protected override IEntity Entity
        {
            get { return this.TenorType; }
            set { this.TenorType = value as TenorType; }
        }
    }
}