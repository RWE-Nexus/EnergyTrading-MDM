namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="LocationRole" />
    /// </summary>
    public class LocationRoleMapping : EntityMapping
    {
        public virtual LocationRole LocationRole { get; set; }

        protected override IEntity Entity
        {
            get { return this.LocationRole; }
            set { this.LocationRole = value as LocationRole; }
        }
    }
}