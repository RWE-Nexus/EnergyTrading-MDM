namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_shapeelement_and_they_exist : IntegrationTestBase
    {
        private static MDM.ShapeElement shapeelement;

        private static RWEST.Nexus.MDM.Contracts.ShapeElement returnedShapeElement;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            shapeelement = ShapeElementData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ShapeElement"] + 
                shapeelement.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedShapeElement = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ShapeElement>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_shapeelement_with_the_correct_details()
        {
            ShapeElementDataChecker.CompareContractWithSavedEntity(returnedShapeElement);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_shapeelement_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.ShapeElement shapeelement;
        private static RWEST.Nexus.MDM.Contracts.ShapeElement returnedShapeElement;
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
            shapeelement = ShapeElementData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["ShapeElement"] + string.Format("{0}?as-of={1}",
                    shapeelement.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedShapeElement = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ShapeElement>();
        }

        [TestMethod]
        public void should_return_the_shapeelement_with_the_correct_details()
        {
            ShapeElementDataChecker.CompareContractWithSavedEntity(returnedShapeElement);
        }
    }
}