namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class CounterpartyDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.CounterpartyDetails>
    {
        public CounterpartyDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.ShortName);
        }
    }
}
