namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Curve" />
    /// </summary>
    public class CurveMapping : EntityMapping
    {
        public virtual Curve Curve { get; set; }

        protected override IEntity Entity
        {
            get { return this.Curve; }
            set { this.Curve = value as Curve; }
        }
    }
}