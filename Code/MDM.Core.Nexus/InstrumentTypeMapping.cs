namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="InstrumentType" />
    /// </summary>
    public class InstrumentTypeMapping : EntityMapping
    {
        public virtual InstrumentType InstrumentType { get; set; }

        protected override IEntity Entity
        {
            get { return this.InstrumentType; }
            set { this.InstrumentType = value as InstrumentType; }
        }
    }
}