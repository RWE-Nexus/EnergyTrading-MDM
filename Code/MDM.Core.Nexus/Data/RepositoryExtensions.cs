namespace EnergyTrading.MDM.Data
{
    using System;
    using System.Linq;

    using EnergyTrading.Data;

    public static class RepositoryExtensions
    {
        public static int FindPartyRoleOverlappingMappingCount<TMapping>(this IRepository repository, string sourceSystem, string mapping, EnergyTrading.DateRange range, string partyRoleType, int mappingId = 0)
            where TMapping : class, IEntityMapping
        {
            var mappings = repository.FindOverlappingMappings<TMapping>(sourceSystem, mapping, range, mappingId);

            return mappings.Cast<PartyRoleMapping>().Count(x => x.PartyRole.PartyRoleType == partyRoleType);
        }

        // TODO: Trying to get calendar working. Need to think a bit more how we deal with updating of calendar days.
        public static MDM.CalendarDay CalendarDayFromCalendar(this IRepository repository, int calendarId, DateTime date)
        {
            return repository.Queryable<MDM.CalendarDay>().Where(day => day.Date.Date == date && day.Calendar.Id == calendarId).First();
        }

        /// <summary>
        /// Locate a LocationRoleType by name
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static LocationRoleType LocationRoleTypeByName(this IRepository repository, string name)
        {
            return repository.Queryable<LocationRoleType>()
                           .Where(x => x.Name == name)
                           .FirstOrDefault();
        }
    }
}
