namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Tenor" />
    /// </summary>
    public class TenorMapping : EntityMapping
    {
        public virtual Tenor Tenor { get; set; }

        protected override IEntity Entity
        {
            get { return this.Tenor; }
            set { this.Tenor = value as Tenor; }
        }
    }
}