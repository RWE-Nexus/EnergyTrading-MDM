namespace EnergyTrading.Mdm.Extensions
{
    using DateRange = EnergyTrading.DateRange;

    public static class DateRangeExtensions
    {
        public static EnergyTrading.Mdm.Contracts.DateRange ToContract(this DateRange value)
        {
            return value == null ? new EnergyTrading.Mdm.Contracts.DateRange() : new EnergyTrading.Mdm.Contracts.DateRange { StartDate = value.Start, EndDate = value.Finish };
        }
    }
}