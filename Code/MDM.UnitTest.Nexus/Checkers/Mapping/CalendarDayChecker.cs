namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.Test;

    public class CalendarDayChecker : Checker<CalendarDay>
    {
        public CalendarDayChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Calendar).Id();
            Compare(x => x.Date);
            Compare(x => x.DayType);
        }
    }
}
