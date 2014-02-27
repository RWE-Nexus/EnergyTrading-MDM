namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class ShapeElementDetailsChecker : Checker<ShapeElementDetails>
    {
        public ShapeElementDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Period.StartDate);
            Compare(x => x.Period.EndDate);
        }
    }
}
