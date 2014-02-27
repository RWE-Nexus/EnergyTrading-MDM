namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM;
    using EnergyTrading.MDM.Data.EF.Actions;

    [TestClass]
    public class CalendarRepositoryFixture : DbSetRepositoryFixture<Calendar>
    {
        protected override Calendar Default()
        {
            var entity = ObjectMother.Create<Calendar>();

            return entity;
        }

        protected override List<Action<IDbSetRepository>> Actions()
        {
            return new List<Action<IDbSetRepository>>() { CalendarActions.CascadeCalendarDay };        
        }
    }
}
