namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="InstrumentType" />
    /// </summary>
    public class FeeTypeMapping : EntityMapping
    {
        public virtual FeeType FeeType { get; set; }

        protected override IEntity Entity

        {
            get { return this.FeeType; }
            set { this.FeeType = value as FeeType; }
        }
    }
}
