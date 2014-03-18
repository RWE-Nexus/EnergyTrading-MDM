namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Calendar" />
    /// </summary>
    public class CalendarMapping : EntityMapping
    {
        public virtual Calendar Calendar { get; set; }

        protected override IEntity Entity
        {
            get { return this.Calendar; }
            set { this.Calendar = value as Calendar; }
        }
    }
}