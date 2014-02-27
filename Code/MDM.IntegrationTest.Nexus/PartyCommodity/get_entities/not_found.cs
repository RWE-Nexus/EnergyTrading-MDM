namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_all_partycommodities : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpResponseMessage response;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["PartyCommodity"] + "list");
            response = client.Get();
        }

        [TestMethod]
        public void should_return_not_found()
        {
            response.AssertStatusCodeIs(HttpStatusCode.NotFound);
        }
    }
}