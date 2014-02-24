namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    [TestClass]
    [Ignore]
    public class when_a_search_for_a_producttypeinstance_is_made_with_a_specific_name : IntegrationTestBase
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

        [TestMethod]
        [Ignore()]
        public void should_return_a_status_code_of_Ok()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [Ignore()]
        public void should_return_the_content_of_the_search_results()
        {
            Assert.IsTrue(response.Headers["Content-Type"].ToLowerInvariant().StartsWith("application/xml"));
        }

        protected static void Because_of()
        {
            client.TransportSettings.MaximumAutomaticRedirections = 0;
            response = client.Post(ServiceUrl["ProductTypeInstance"] + "Search", content);
        }

        protected static void Establish_context()
        {
            var entity1 = Script.ProductTypeInstanceData.CreateBasicEntity();
            var entity2 = Script.ProductTypeInstanceData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search();
			Script.ProductTypeInstanceData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(RWEST.Nexus.Contracts.Search.SearchExtensions.ToNexus(search));
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Split('/');
        }
    }
}
