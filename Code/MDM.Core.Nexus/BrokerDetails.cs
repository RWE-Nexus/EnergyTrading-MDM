namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;

    public class BrokerDetails : PartyRoleDetails, IIdentifiable, IEntityDetail
    {
        public virtual string Fax { get; set; }

        public virtual string Phone { get; set; }

        public virtual decimal Rate { get; set; }
    }
}
