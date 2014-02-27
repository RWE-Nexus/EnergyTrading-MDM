namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading.Data;

    public class Broker : PartyRole, IIdentifiable, IEntity
    {
        protected override void CopyAdditionalDetails(PartyRoleDetails details)
        {
            var latestBrokerDetails = (BrokerDetails)this.LatestDetails;
            var brokerDetails = (BrokerDetails)details;

            latestBrokerDetails.Phone = brokerDetails.Phone;
            latestBrokerDetails.Fax = brokerDetails.Fax;
            latestBrokerDetails.Rate = brokerDetails.Rate;
        }
    }
}
