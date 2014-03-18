namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class DateRangeChecker : Checker<EnergyTrading.Mdm.Contracts.DateRange>
    {
        public DateRangeChecker()
        {
            Compare(x => x.StartDate);
            Compare(x => x.EndDate);
        }
    }
}