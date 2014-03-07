namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class LocationRoleChecker : Checker<LocationRole>
    {
        public LocationRoleChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Type).Id();
            Compare(x => x.Location).Id();
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}