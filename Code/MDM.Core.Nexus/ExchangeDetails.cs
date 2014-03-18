namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Data;

    public class ExchangeDetails : PartyRoleDetails, IIdentifiable, IEntityDetail
    {
        public virtual string Phone { get; set; }

        public virtual string Fax { get; set; }
    }
}
