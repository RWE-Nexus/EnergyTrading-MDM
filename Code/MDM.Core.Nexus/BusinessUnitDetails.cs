namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;

    public class BusinessUnitDetails : PartyRoleDetails, IIdentifiable, IEntityDetail
    {
        public string Phone { get; set; }

        public string Fax { get; set; }

        public virtual Location TaxLocation { get; set; }

        public string AccountType { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }
    }
}
