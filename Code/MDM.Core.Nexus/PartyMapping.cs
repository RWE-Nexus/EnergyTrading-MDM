namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Party" />
    /// </summary>
    public class PartyMapping : EntityMapping
    {
        public virtual Party Party { get; set; }

        protected override IEntity Entity
        {
            get { return this.Party; }
            set { this.Party = value as Party; }
        }
    }
}