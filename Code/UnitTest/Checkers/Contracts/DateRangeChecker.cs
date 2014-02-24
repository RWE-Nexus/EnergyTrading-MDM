namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class DateRangeChecker : Checker<RWEST.Nexus.MDM.Contracts.DateRange>
    {
        public DateRangeChecker()
        {
            Compare(x => x.StartDate);
            Compare(x => x.EndDate);
        }
    }
}