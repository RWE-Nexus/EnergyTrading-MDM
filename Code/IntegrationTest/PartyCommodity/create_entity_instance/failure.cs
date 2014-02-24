namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_create_a_partycommodity_entity : IntegrationTestBase
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
            content = HttpContentExtensions.CreateDataContract(new RWEST.Nexus.MDM.Contracts.PartyCommodity());
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["PartyCommodity"], content);
        }

        [TestMethod]
        public void should_return_error()
        {
            response.AssertStatusCodeIs(HttpStatusCode.InternalServerError);
        }
    }
}