namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class BrokerRateDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.BrokerRateDetails>
    {
        public BrokerRateDetailsChecker()
        {
            Compare(x => x.Broker);
            Compare(x => x.Desk);
            Compare(x => x.CommodityInstrumentType);
            Compare(x => x.Location);
            Compare(x => x.ProductType);
            Compare(x => x.PartyAction);
            Compare(x => x.Rate);
        }
    }
}
