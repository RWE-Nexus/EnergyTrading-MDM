namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    using Calendar = EnergyTrading.MDM.Calendar;
    using CalendarDay = EnergyTrading.MDM.CalendarDay;

    public partial class CalendarData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Calendar contract)
        {
            var calendar = ObjectMother.Create<Calendar>();
            this.repository.Add(calendar);
            this.repository.Flush();

            var days = new CalendarDayList();

            if(contract.Details != null && contract.Details.CalendayDays != null)
            {
                days = contract.Details.CalendayDays;
            }

            contract.Details = new CalendarDetails() { Name = Guid.NewGuid().ToString(), CalendayDays = days };
        }

        partial void AddDetailsToEntity(Calendar entity, DateTime startDate, DateTime endDate)
        {
            entity.Name = Guid.NewGuid().ToString();
        }

        partial void  CreateSearchData(Search search, Calendar entity1, Calendar entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}