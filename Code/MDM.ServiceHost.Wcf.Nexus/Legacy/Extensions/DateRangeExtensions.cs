namespace EnergyTrading.MDM.LegacyExtensions
{
    public static class DateRangeExtensions
    {
        public static RWEST.Nexus.MDM.Contracts.DateRange ToContract(this DateRange value)
        {
            return value == null ? new RWEST.Nexus.MDM.Contracts.DateRange() : new RWEST.Nexus.MDM.Contracts.DateRange { StartDate = value.Start, EndDate = value.Finish };
        }
    }
}