namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Party" />
    /// </summary>
    public class PartyAccountabilityMapping : EntityMapping
    {
        public virtual PartyAccountability PartyAccountability { get; set; }

        protected override IEntity Entity
        {
            get { return this.PartyAccountability; }
            set { this.PartyAccountability = value as PartyAccountability; }
        }
    }
}
