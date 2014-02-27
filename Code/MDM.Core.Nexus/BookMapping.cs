namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Book" />
    /// </summary>
    public class BookMapping : EntityMapping
    {
        public virtual Book Book { get; set; }

        protected override IEntity Entity
        {
            get { return this.Book; }
            set { this.Book = value as Book; }
        }
    }
}