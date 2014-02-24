namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BrokerRateChecker : Checker<BrokerRate>
    {
        public BrokerRateChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Details).Count();
            Compare(x => x.Mappings).Count();
            
        }
    }
}
