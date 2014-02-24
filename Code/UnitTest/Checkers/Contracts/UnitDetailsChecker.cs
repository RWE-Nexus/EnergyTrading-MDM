namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class UnitDetailsChecker : Checker<UnitDetails>
    {
        public UnitDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Description);
            Compare(x => x.Dimension);
            Compare(x => x.ConversionConstant);
            Compare(x => x.ConversionFactor);
            Compare(x => x.Symbol);
        }
    }
}
