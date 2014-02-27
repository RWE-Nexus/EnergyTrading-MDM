namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_shape_and_they_exist : IntegrationTestBase
    {
        private static MDM.Shape shape;

        private static RWEST.Nexus.MDM.Contracts.Shape returnedShape;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            shape = ShapeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Shape"] + 
                shape.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedShape = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Shape>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_shape_with_the_correct_details()
        {
            ShapeDataChecker.CompareContractWithSavedEntity(returnedShape);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_shape_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Shape shape;
        private static RWEST.Nexus.MDM.Contracts.Shape returnedShape;
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
            shape = ShapeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Shape"] + string.Format("{0}?as-of={1}",
                    shape.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedShape = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Shape>();
        }

        [TestMethod]
        public void should_return_the_shape_with_the_correct_details()
        {
            ShapeDataChecker.CompareContractWithSavedEntity(returnedShape);
        }
    }
}