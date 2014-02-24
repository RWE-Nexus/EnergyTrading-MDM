namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Hierarchy" />
    /// </summary>
    public class HierarchyMapping : EntityMapping
    {
        public virtual Hierarchy Hierarchy { get; set; }

        protected override IEntity Entity
        {
            get { return this.Hierarchy; }
            set { this.Hierarchy = value as Hierarchy; }
        }
    }
}