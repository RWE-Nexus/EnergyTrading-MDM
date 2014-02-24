namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class LocationChecker : Checker<Location>
    {
        public LocationChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}