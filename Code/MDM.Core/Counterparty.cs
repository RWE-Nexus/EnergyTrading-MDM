namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading.Data;

    public class Counterparty : PartyRole, IIdentifiable, IEntity
    {
        protected override void CopyAdditionalDetails(PartyRoleDetails details)
        {
            var latestCounterpartyDetails = (CounterpartyDetails)this.LatestDetails;
            var counterpartyDetails = (CounterpartyDetails)details;

            latestCounterpartyDetails.Phone = counterpartyDetails.Phone;
            latestCounterpartyDetails.Fax = counterpartyDetails.Fax;
            latestCounterpartyDetails.ShortName = counterpartyDetails.ShortName;
        }
    }
}

