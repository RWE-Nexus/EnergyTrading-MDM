namespace EnergyTrading.MDM
{
    using System.Collections.Generic;

    public partial class Calendar
    {
        partial void OnCreate()
        {
            Days = new List<CalendarDay>();
        }

        public string Name { get; set; }

        public virtual IList<CalendarDay> Days { get; set; }

        partial void CopyDetails(Calendar details)
        {
            this.Name = details.Name;

            if (details.Days.Count > 0)
            {
                this.Days.Clear();
            }

            foreach(var day in details.Days)
            {
                day.Calendar = this;
                Days.Add(day);
            }
        }
    }
}