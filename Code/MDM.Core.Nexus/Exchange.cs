namespace EnergyTrading.MDM
{
    using System.Collections.Generic;

    using EnergyTrading.Data;

    public class Exchange : PartyRole, IIdentifiable, IEntity
    {
        protected override void CopyAdditionalDetails(PartyRoleDetails details)
        {
            var latestExchangeDetails = (ExchangeDetails)this.LatestDetails;
            var exchangeDetails = (ExchangeDetails)details;

            latestExchangeDetails.Phone = exchangeDetails.Phone;
            latestExchangeDetails.Fax = exchangeDetails.Fax;
        }
    }
}
