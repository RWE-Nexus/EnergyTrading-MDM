namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_search_for_a_partycommodity_is_made : IntegrationTestBase
    {
        private static HttpClient client;
        private static HttpContent content;
        private static HttpResponseMessage response;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient();
            content = HttpContentExtensions.CreateDataContract(new RWEST.Nexus.Contracts.Search.Search());

            response = client.Post(ServiceUrl["PartyCommodity"] + "Search", content);
        }

        [TestMethod]
        public void should_return_not_found()
        {
            response.AssertStatusCodeIs(HttpStatusCode.NotFound);
        }
    }
}
