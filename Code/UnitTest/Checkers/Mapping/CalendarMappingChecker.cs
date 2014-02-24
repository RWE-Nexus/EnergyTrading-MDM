namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CalendarMappingChecker : Checker<CalendarMapping>
    {
        public CalendarMappingChecker()
        {
            Compare(x => x.Calendar).Id();
        }
    }
}
