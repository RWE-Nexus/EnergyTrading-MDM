namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_calendar_and_they_exist : IntegrationTestBase
    {
        private static MDM.Calendar calendar;

        private static RWEST.Nexus.MDM.Contracts.Calendar returnedCalendar;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            calendar = Script.CalendarData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Calendar"] + 
                calendar.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCalendar = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Calendar>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_calendar_with_the_correct_details()
        {
            Script.CalendarDataChecker.CompareContractWithSavedEntity(returnedCalendar);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_calendar_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Calendar calendar;
        private static RWEST.Nexus.MDM.Contracts.Calendar returnedCalendar;
        private static DateTime asof;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            calendar = Script.CalendarData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Calendar"] + string.Format("{0}?as-of={1}",
                    calendar.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedCalendar = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Calendar>();
        }

        [TestMethod]
        public void should_return_the_calendar_with_the_correct_details()
        {
            Script.CalendarDataChecker.CompareContractWithSavedEntity(returnedCalendar);
        }
    }
}