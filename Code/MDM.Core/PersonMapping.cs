namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Person" />
    /// </summary>
    public class PersonMapping : EntityMapping
    {
        public virtual Person Person { get; set; }

        protected override IEntity Entity
        {
            get { return this.Person; }
            set { this.Person = value as Person; }
        }
    }
}