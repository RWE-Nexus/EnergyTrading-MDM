namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class VesselMappingChecker : Checker<VesselMapping>
    {
        public VesselMappingChecker()
        {
            Compare(x => x.Vessel).Id();
        }
    }
}
