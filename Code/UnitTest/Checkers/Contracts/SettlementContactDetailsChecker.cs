namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;
    using EnergyTrading.Test;

    public class SettlementContactDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.SettlementContactDetails>
    {
        public SettlementContactDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}
