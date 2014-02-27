namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ShipperCode" />
    /// </summary>
    public class ShipperCodeMapping : EntityMapping
    {
        public virtual ShipperCode ShipperCode { get; set; }

        protected override IEntity Entity
        {
            get { return this.ShipperCode; }
            set { this.ShipperCode = value as ShipperCode; }
        }
    }
}