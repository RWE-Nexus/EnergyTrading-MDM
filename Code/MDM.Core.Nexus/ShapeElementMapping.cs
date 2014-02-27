namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ShapeElement" />
    /// </summary>
    public class ShapeElementMapping : EntityMapping
    {
        public virtual ShapeElement ShapeElement { get; set; }

        protected override IEntity Entity
        {
            get { return this.ShapeElement; }
            set { this.ShapeElement = value as ShapeElement; }
        }
    }
}