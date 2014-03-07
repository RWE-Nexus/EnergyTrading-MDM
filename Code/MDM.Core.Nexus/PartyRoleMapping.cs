namespace EnergyTrading.MDM
{
    public class PartyRoleMapping : EntityMapping
    {
        public virtual PartyRole PartyRole { get; set; }

        protected override IEntity Entity
        {
            get { return this.PartyRole; }
            set { this.PartyRole = value as PartyRole; }
        }
    }
}