namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_to_update_a_partycommodity_entity : IntegrationTestBase
    {
        private static HttpClient client;
        private static HttpContent content;
        private static HttpResponseMessage response;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            content = HttpContentExtensions.CreateDataContract(new PartyCommodity());
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["PartyCommodity"] + "1", content);
        }

        [TestMethod]
        public void should_return_error()
        {
            response.AssertStatusCodeIs(HttpStatusCode.InternalServerError);
        }
    }
}
