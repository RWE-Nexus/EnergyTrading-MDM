namespace EnergyTrading.MDM
{
    public class PartyRoleAccountabilityMapping : EntityMapping
    {
        public virtual PartyRoleAccountability PartyRoleAccountability { get; set; }

        protected override IEntity Entity
        {
            get { return this.PartyRoleAccountability; }
            set { this.PartyRoleAccountability = value as PartyRoleAccountability; }
        }
    }
}