namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_to_create_a_producttype_entity_with_a_null_deliveryrangetype : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static RWEST.Nexus.MDM.Contracts.ProductType producttype;
        private static HttpContent content;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            producttype = Script.ProductTypeData.CreateContractForEntityCreation();
            producttype.Details.DeliveryRangeType = null;

            content = HttpContentExtensions.CreateDataContract(producttype);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["ProductType"], content);
        }

        [TestMethod]
        public void should_return_an_created_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void should_display_relevant_error_message()
        {
            Assert.IsTrue(response.Content.ReadAsDataContract<Fault>().Message.Contains("Delivery Range Type must not be null"));
        }
    }
}
