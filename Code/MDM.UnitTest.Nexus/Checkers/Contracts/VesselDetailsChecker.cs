namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class VesselDetailsChecker : Checker<VesselDetails>
    {
        public VesselDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}
