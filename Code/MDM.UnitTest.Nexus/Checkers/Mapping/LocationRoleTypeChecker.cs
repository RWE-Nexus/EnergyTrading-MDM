namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.Test;

    class LocationRoleTypeChecker : Checker<LocationRoleType>
    {
        public LocationRoleTypeChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
        }
    }
}