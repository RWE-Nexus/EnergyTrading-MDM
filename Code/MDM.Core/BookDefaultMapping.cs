namespace EnergyTrading.MDM
{
    using RWEST.Nexus.MDM.Contracts;

    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="BookDefault" />
    /// </summary>
    public class BookDefaultMapping : EntityMapping
    {
        public virtual BookDefault BookDefault { get; set; }

        protected override IEntity Entity
        {
            get { return this.BookDefault; }
            set { this.BookDefault = value as BookDefault; }
        }
    }
}