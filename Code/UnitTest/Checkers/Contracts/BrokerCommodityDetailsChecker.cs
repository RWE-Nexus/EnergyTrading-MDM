namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class BrokerCommodityDetailsChecker : Checker<BrokerCommodityDetails>
    {
        public BrokerCommodityDetailsChecker()
        {
            Compare(x => x.Broker);
            Compare(x => x.Commodity);
            Compare(x => x.Name);
            
        }
    }
}
