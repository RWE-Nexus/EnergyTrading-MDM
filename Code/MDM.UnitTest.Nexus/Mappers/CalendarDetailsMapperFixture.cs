namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    using CalendarDay = EnergyTrading.MDM.CalendarDay;

    [TestClass]
    public class CalendarDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.Calendar()
                {
                    Name = "calendar name",
                };

            var calendarDays = new List<CalendarDay>
                {
                    new CalendarDay() { Date = DateTime.Today, DayType = 1 }
                };

            source.Days = calendarDays;

            var mapper = new MDM.Mappers.CalendarDetailsMapper();

            // Act
            var result = mapper.Map(source);

			// Assert
			Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Days.Count, result.CalendayDays.Count);
            Assert.AreEqual(DayType.Weekend, result.CalendayDays[0].CalendarDayType);
        }
    }
}

	