namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;

    public partial class BusinessUnit : PartyRole, IIdentifiable, IEntity
    {
        protected override void CopyAdditionalDetails(PartyRoleDetails details)
        {
            var latestBusinessUnitDetails = (BusinessUnitDetails)this.LatestDetails;
            // force the load of related entiies to make sure that updating these to null deletes the relationship in EF
            var forceLoadTaxLocation = latestBusinessUnitDetails.TaxLocation;

            var newBusinessUnitDetails = (BusinessUnitDetails)details;

            latestBusinessUnitDetails.Phone = newBusinessUnitDetails.Phone;
            latestBusinessUnitDetails.Fax = newBusinessUnitDetails.Fax;
            latestBusinessUnitDetails.TaxLocation = newBusinessUnitDetails.TaxLocation;
            latestBusinessUnitDetails.AccountType = newBusinessUnitDetails.AccountType;
            latestBusinessUnitDetails.Address = newBusinessUnitDetails.Address;
            latestBusinessUnitDetails.Status = newBusinessUnitDetails.Status;
        }
    }
}
