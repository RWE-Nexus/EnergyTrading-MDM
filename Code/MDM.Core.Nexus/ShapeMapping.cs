namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Shape" />
    /// </summary>
    public class ShapeMapping : EntityMapping
    {
        public virtual Shape Shape { get; set; }

        protected override IEntity Entity
        {
            get { return this.Shape; }
            set { this.Shape = value as Shape; }
        }
    }
}