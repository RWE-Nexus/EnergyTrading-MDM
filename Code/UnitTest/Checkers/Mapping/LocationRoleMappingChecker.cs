namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class LocationRoleMappingChecker : Checker<LocationRoleMapping>
    {
        public LocationRoleMappingChecker()
        {
            Compare(x => x.LocationRole).Id();
        }
    }
}
