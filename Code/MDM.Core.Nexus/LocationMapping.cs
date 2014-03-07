namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Location" />
    /// </summary>
    public class LocationMapping : EntityMapping
    {
        public virtual Location Location { get; set; }

        protected override IEntity Entity
        {
            get { return this.Location; }
            set { this.Location = value as Location; }
        }
    }
}