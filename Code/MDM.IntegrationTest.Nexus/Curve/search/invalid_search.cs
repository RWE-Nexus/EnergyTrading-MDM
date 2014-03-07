namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.Contracts.Search;
    using RWEST.Nexus.Search;

    [TestClass]
    public class when_a_search_for_a_curve_is_made_with_an_invalid_search_field_name : IntegrationTestBase
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
        public void should_return_a_status_code_of_not_found()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void should_not_return_a_location_for_the_search()
        {
            Assert.IsFalse(response.Headers.ContainsKey("Location"), "Location shouldn't be added to the header values");
        }

        protected static void Because_of()
        {
            client.TransportSettings.MaximumAutomaticRedirections = 0;
            response = client.Post(ServiceUrl["Curve"] + "Search", content);
        }

        protected static void Establish_context()
        {
            client = new HttpClient();

            Search search = SearchBuilder.CreateSearch();
            search.AddSearchCriteria(SearchCombinator.Or).AddCriteria(
                "InvalidField", SearchCondition.Equals, Guid.NewGuid().ToString());

            content = HttpContentExtensions.CreateDataContract(search);
        }
    }
}


