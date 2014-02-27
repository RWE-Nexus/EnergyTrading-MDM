namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Unit" />
    /// </summary>
    public class UnitMapping : EntityMapping
    {
        public virtual Unit Unit { get; set; }

        protected override IEntity Entity
        {
            get { return this.Unit; }
            set { this.Unit = value as Unit; }
        }
    }
}