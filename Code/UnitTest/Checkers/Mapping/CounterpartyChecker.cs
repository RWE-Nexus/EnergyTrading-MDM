namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CounterpartyChecker : Checker<Counterparty>
    {
        public CounterpartyChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Details).Count();
            Compare(x => x.Mappings).Count();
        }
    }
}
