namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading;

    [TestClass]
    public class CalendarDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.CalendarDetails
                {
                    CalendayDays = new CalendarDayList(), 
                    Name = "Test Name"
                };

            source.CalendayDays.Add(new CalendarDay() { CalendarDate = DateUtility.Round(SystemTime.UtcNow()) , CalendarDayType = DayType.Weekend});

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.CalendarDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.CalendayDays[0].CalendarDate, result.Days[0].Date);
            Assert.AreEqual(1, result.Days[0].DayType);
        }
    }
}
		