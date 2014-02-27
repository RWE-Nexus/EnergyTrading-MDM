namespace EnergyTrading.MDM
{
    /// <summary>
    /// Maps a <see cref="EntityMapping" /> to a <see cref="Agreement" />
    /// </summary>
    public class AgreementMapping : EntityMapping
    {
        public virtual Agreement Agreement { get; set; }

        protected override IEntity Entity
        {
            get { return this.Agreement; }
            set { this.Agreement = value as Agreement; }
        }
    }
}