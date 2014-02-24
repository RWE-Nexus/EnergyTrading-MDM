namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class LocationMappingChecker : Checker<LocationMapping>
    {
        public LocationMappingChecker()
        {
            Compare(x => x.Location).Id();
        }
    }
}
