namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="InstrumentTypeOverride" />
    /// </summary>
    public class InstrumentTypeOverrideMapping : EntityMapping
    {
        public virtual InstrumentTypeOverride InstrumentTypeOverride { get; set; }

        protected override IEntity Entity
        {
            get { return this.InstrumentTypeOverride; }
            set { this.InstrumentTypeOverride = value as InstrumentTypeOverride; }
        }
    }
}