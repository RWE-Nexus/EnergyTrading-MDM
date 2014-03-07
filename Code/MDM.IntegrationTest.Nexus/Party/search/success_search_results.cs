namespace EnergyTrading.MDM.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass, Ignore]
    public class when_a_search_for_a_party_is_made_with_a_specific_name_and_results_are_found : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static MDM.Party entity1;

        private static MDM.Party entity2;

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

            List<RWEST.Nexus.MDM.Contracts.Party> result =
                feed.Items.Select(syndicationItem => (XmlSyndicationContent)syndicationItem.Content).Select(
                    syndic => syndic.ReadContent<RWEST.Nexus.MDM.Contracts.Party>()).ToList();

            Assert.AreEqual(1, result.Where(x => x.ToNexusKey() == entity1.Id).Count(), string.Format("Entity not found in search results {0}", entity1.Id));
            Assert.AreEqual(1, result.Where(x => x.ToNexusKey() == entity2.Id).Count(), string.Format("Entity not found in search results {0}", entity2.Id));
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["Party"] + "search", content);
        }

        protected static void Establish_context()
        {
            entity1 = Script.PartyData.CreateBasicEntity();
            entity2 = Script.PartyData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search();
			Script.PartyData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(RWEST.Nexus.Contracts.Search.SearchExtensions.ToNexus(search));
        }
    }
}
