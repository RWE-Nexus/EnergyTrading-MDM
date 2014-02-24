namespace RWEST.Nexus.MDM.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using RWEST.Nexus.Search;

    [TestClass]
    public class when_a_search_for_a_curve_is_made_with_a_mapping_value_and_results_are_found : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static MDM.Curve entity1;

        private static MDM.Curve entity2;

        private static HttpResponseMessage response;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        [TestMethod]
        public void should_return_the_ok_status_code()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void should_return_the_relevant_search_results()
        {
            XmlReader reader = XmlReader.Create(
                response.Content.ReadAsStream(), new XmlReaderSettings { ProhibitDtd = false });
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            List<Contracts.Curve> result =
                feed.Items.Select(syndicationItem => (XmlSyndicationContent)syndicationItem.Content).Select(
                    syndic => syndic.ReadContent<Contracts.Curve>()).ToList();

            Assert.AreEqual(1, result.Where(x => x.ToNexusKey() == entity1.Id).Count(), string.Format("Entity not found in search results {0}", entity1.Id));
            Assert.AreEqual(1, result.Where(x => x.ToNexusKey() == entity2.Id).Count(), string.Format("Entity not found in search results {0}", entity2.Id));
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["Curve"] + "search", content);
        }

        protected static void Establish_context()
        {
            entity1 = CurveData.CreateBasicEntityWithOneMapping();
            entity2 = CurveData.CreateBasicEntityWithOneMapping();

            client = new HttpClient();

            Search search = SearchBuilder.CreateSearch(IsMappingSearch: true);
            search.AddSearchCriteria(SearchCombinator.Or).AddCriteria(
                "MappingValue", SearchCondition.Equals, entity1.Mappings[0].MappingValue).AddCriteria(
                    "MappingValue", SearchCondition.Equals, entity2.Mappings[0].MappingValue);

            content = HttpContentExtensions.CreateDataContract(search);
        }
    }

    [TestClass]
    public class when_a_search_for_a_curve_is_made_with_a_mapping_value_and_a_system_name_and_results_are_found : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static MDM.Curve entity1;

        private static HttpResponseMessage response;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        [TestMethod]
        public void should_return_the_ok_status_code()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void should_return_the_relevant_search_results()
        {
            XmlReader reader = XmlReader.Create(
                response.Content.ReadAsStream(), new XmlReaderSettings { ProhibitDtd = false });
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            List<Contracts.Curve> result =
                feed.Items.Select(syndicationItem => (XmlSyndicationContent)syndicationItem.Content).Select(
                    syndic => syndic.ReadContent<Contracts.Curve>()).ToList();

            Assert.AreEqual(1, result.Where(x => x.ToNexusKey() == entity1.Id).Count(), string.Format("Entity not found in search results {0}", entity1.Id));
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["Curve"] + "search", content);
        }

        protected static void Establish_context()
        {
            entity1 = CurveData.CreateBasicEntityWithOneMapping();

            client = new HttpClient();

            Search search = SearchBuilder.CreateSearch(IsMappingSearch: true);
            search.AddSearchCriteria(SearchCombinator.And).AddCriteria(
                "MappingValue", SearchCondition.Equals, entity1.Mappings[0].MappingValue).AddCriteria(
                    "System.Name", SearchCondition.Equals, "Endur");

            content = HttpContentExtensions.CreateDataContract(search);
        }
    }
}
