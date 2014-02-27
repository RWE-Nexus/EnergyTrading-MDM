namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CalendarChecker : Checker<Calendar>
    {
        public CalendarChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Validity);
            Compare(x => x.Days);
            Compare(x => x.Mappings).Count();
        }
    }
}
