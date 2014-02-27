namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.Contracts.Search;
    using RWEST.Nexus.Search;

    [TestClass]
    public class when_a_search_for_a_curve_is_made_with_a_specific_name : IntegrationTestBase
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
        public void should_return_a_status_code_of_moved()
        {
            Assert.AreEqual(HttpStatusCode.Moved, response.StatusCode);
        }

        [TestMethod]
        public void should_return_the_location_of_the_search()
        {
            Assert.AreEqual("Search", this.GetLocationHeader()[0], true);

            Guid id;
            bool parsedGuid = Guid.TryParse(this.GetLocationHeader()[1], out id);
            Assert.IsTrue(parsedGuid, "The id returned was not a guid");
        }

        protected static void Because_of()
        {
            client.TransportSettings.MaximumAutomaticRedirections = 0;
            response = client.Post(ServiceUrl["Curve"] + "Search", content);
        }

        protected static void Establish_context()
        {
            var entity1 = CurveData.CreateBasicEntity();
            var entity2 = CurveData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search();
			CurveData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(search);
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Split('/');
        }
    }
}
