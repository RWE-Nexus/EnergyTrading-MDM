namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class VesselChecker : Checker<Vessel>
    {
        public VesselChecker()
        {
            Compare(x => x.Id);
			Compare(x => x.Name);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
