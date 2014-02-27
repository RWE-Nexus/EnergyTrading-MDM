namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="PartyOverride" />
    /// </summary>
    public class PartyOverrideMapping : EntityMapping
    {
        public virtual PartyOverride PartyOverride { get; set; }

        protected override IEntity Entity
        {
            get { return this.PartyOverride; }
            set { this.PartyOverride = value as PartyOverride; }
        }
    }
}