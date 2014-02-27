namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Data;

    public class CalendarDay : IIdentifiable
    {
        private DateTime date;

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual Calendar Calendar { get; set; }

        public DateTime Date
        {
            get { return date; }
            set { date = DateUtility.Round(value); }
        }

        public int DayType { get; set; }
    }
}
