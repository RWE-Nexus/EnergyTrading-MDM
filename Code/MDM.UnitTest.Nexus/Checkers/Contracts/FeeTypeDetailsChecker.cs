namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class FeeTypeDetailsChecker : Checker<FeeTypeDetails>
    {
        public FeeTypeDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}
