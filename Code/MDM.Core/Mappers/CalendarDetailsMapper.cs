namespace EnergyTrading.MDM.Mappers
{
    using System;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    using Calendar = EnergyTrading.MDM.Calendar;

    public class CalendarDetailsMapper : Mapper<EnergyTrading.MDM.Calendar, CalendarDetails>
    {
        public override void Map(EnergyTrading.MDM.Calendar source, CalendarDetails destination)
        {
            foreach(var day in source.Days)
            {
                destination.CalendayDays.Add(
                    new CalendarDay
                        {
                            CalendarDate = day.Date,
                            CalendarDayType = (DayType)Enum.ToObject(typeof(DayType), day.DayType)
                        });
            }

            destination.Name = source.Name; 
        }
    }
}
		