namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BrokerCommodityChecker : Checker<BrokerCommodity>
    {
        public BrokerCommodityChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Name);
            Compare(x => x.Broker);
            Compare(x => x.Commodity);
            
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
