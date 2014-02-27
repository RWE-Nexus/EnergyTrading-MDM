namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class BrokerDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.BrokerDetails>
    {
        public BrokerDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.Rate);
        }
    }
}
