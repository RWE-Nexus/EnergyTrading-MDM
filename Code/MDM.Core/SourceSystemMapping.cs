namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="SourceSystem" />
    /// </summary>
    public class SourceSystemMapping : EntityMapping
    {
        public virtual SourceSystem SourceSystem { get; set; }

        protected override IEntity Entity
        {
            get { return this.SourceSystem; }
            set { this.SourceSystem = value as SourceSystem; }
        }
    }
}