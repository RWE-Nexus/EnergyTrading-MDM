namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BrokerRateDetailsChecker : Checker<BrokerRateDetails>
    {
        public BrokerRateDetailsChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.BrokerRate).Id();
            Compare(x => x.Broker);
            Compare(x => x.Desk);
            Compare(x => x.Location);
            Compare(x => x.CommodityInstrumentType);
            Compare(x => x.ProductType);
            Compare(x => x.PartyAction);
            Compare(x => x.Rate);
            
            Compare(x => x.Validity);
           
        }
    }
}
