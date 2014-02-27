namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_shapeday_and_they_exist : IntegrationTestBase
    {
        private static MDM.ShapeDay shapeday;

        private static RWEST.Nexus.MDM.Contracts.ShapeDay returnedShapeDay;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            shapeday = ShapeDayData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ShapeDay"] + 
                shapeday.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedShapeDay = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ShapeDay>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_shapeday_with_the_correct_details()
        {
            ShapeDayDataChecker.CompareContractWithSavedEntity(returnedShapeDay);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_shapeday_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.ShapeDay shapeday;
        private static RWEST.Nexus.MDM.Contracts.ShapeDay returnedShapeDay;
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
            shapeday = ShapeDayData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["ShapeDay"] + string.Format("{0}?as-of={1}",
                    shapeday.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedShapeDay = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ShapeDay>();
        }

        [TestMethod]
        public void should_return_the_shapeday_with_the_correct_details()
        {
            ShapeDayDataChecker.CompareContractWithSavedEntity(returnedShapeDay);
        }
    }
}