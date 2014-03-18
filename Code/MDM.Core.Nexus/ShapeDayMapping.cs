namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="ShapeDay" />
    /// </summary>
    public class ShapeDayMapping : EntityMapping
    {
        public virtual ShapeDay ShapeDay { get; set; }

        protected override IEntity Entity
        {
            get { return this.ShapeDay; }
            set { this.ShapeDay = value as ShapeDay; }
        }
    }
}