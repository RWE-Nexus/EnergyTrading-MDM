namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Vessel" />
    /// </summary>
    public class VesselMapping : EntityMapping
    {
        public virtual Vessel Vessel { get; set; }

        protected override IEntity Entity
        {
            get { return this.Vessel; }
            set { this.Vessel = value as Vessel; }
        }
    }
}