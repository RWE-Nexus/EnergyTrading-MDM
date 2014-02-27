using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    [TestClass]
    public class when_a_search_for_a_product_is_made_mapping_with_a_system_name : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static HttpResponseMessage response;
        private static Product endurOnlyEntity;
        private static Product trayportAndEndurEntity;

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
        //TODO: RobH Timing issue needs to be repaired
        public void should_return_the_relevant_search_results()
        {
            XmlReader reader = XmlReader.Create(
                response.Content.ReadAsStream(), new XmlReaderSettings { ProhibitDtd = false });
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            List<RWEST.Nexus.MDM.Contracts.Product> result =
                feed.Items.Select(syndicationItem => (XmlSyndicationContent)syndicationItem.Content).Select(
                    syndic => syndic.ReadContent<RWEST.Nexus.MDM.Contracts.Product>()).ToList();

            Assert.AreEqual(1, result.Where(x => x.ToNexusKey() == trayportAndEndurEntity.Id).Count(), string.Format("Entity not found in search results {0}", trayportAndEndurEntity.Id));
            Assert.AreEqual(1, result.Count);
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["Product"] + "search", content);
        }

        protected static void Establish_context()
        {
            endurOnlyEntity = Script.ProductData.CreateBasicEntityWithOneMapping();
            trayportAndEndurEntity = Script.ProductData.CreateEntityWithTwoDetailsAndTwoMappings();

            client = new HttpClient();

            var search = SearchBuilder.CreateSearch(isMappingSearch: true);
            search.AddSearchCriteria(SearchCombinator.And)
                  .AddCriteria("System.Name", SearchCondition.Equals, "trayport");
            search.SearchOptions.ResultsPerPage = null;
            search.SearchOptions.MultiPage = false;

            content = HttpContentExtensions.CreateDataContract(RWEST.Nexus.Contracts.Search.SearchExtensions.ToNexus(search));
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Split('/');
        }
    }
}

