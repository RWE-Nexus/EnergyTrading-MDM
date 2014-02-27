namespace EnergyTrading.MDM.Data.EF.Actions
{
    using EnergyTrading.Data.EntityFramework;

    public static class CalendarActions
    {
        public static void CascadeCalendarDay(IDbSetRepository repository)
        {
            repository.DbSet<CalendarDay>().RemoveLocals(x => x.Calendar == null);
        }
    }
}