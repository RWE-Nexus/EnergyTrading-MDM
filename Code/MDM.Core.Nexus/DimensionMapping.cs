namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Dimension" />
    /// </summary>
    public class DimensionMapping : EntityMapping
    {
        public virtual Dimension Dimension { get; set; }

        protected override IEntity Entity
        {
            get { return this.Dimension; }
            set { this.Dimension = value as Dimension; }
        }
    }
}