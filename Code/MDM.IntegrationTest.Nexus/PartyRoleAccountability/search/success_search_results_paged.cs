	namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;

    [TestClass]
        public class when_a_search_for_a_partyRoleaccountability_with_two_results_is_paged_at_one_per_page : IntegrationTestBase
    {
        private static HttpClient client;

        private static HttpContent content;

        private static MDM.PartyRoleAccountability entity1;
        private static MDM.PartyRoleAccountability entity2;

        private static IList<PartyRoleAccountability> pageOne;
        private static Uri pageOneNextPage;
        private static IList<PartyRoleAccountability> pageTwo;
        private static Uri pageTwoNextPage;
        private static HttpResponseMessage pageOneResponse;
        private static HttpResponseMessage pageTwoResponse;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        [TestMethod]
        public void should_return_the_ok_status_code()
        {
            Assert.AreEqual(HttpStatusCode.OK, pageOneResponse.StatusCode);
        }

        [TestMethod]
        public void should_have_one_of_the_results_in_the_first_page()
        {
            Assert.AreEqual(1, pageOne.Where(x => x.ToNexusKey() == entity1.Id || x.ToNexusKey() == entity2.Id).Count());
        }

        [TestMethod]
        public void should_have_one_of_the_results_in_the_second_page()
        {
            Assert.AreEqual(1, pageTwo.Where(x => x.ToNexusKey() == entity1.Id || x.ToNexusKey() == entity2.Id).Count());
        }

        [TestMethod]
        public void should_return_the_second_page_of_results_as_a_link()
        {
            Assert.IsNotNull(pageOneNextPage);
        }

        [TestMethod]
        public void should_not_have_a_third_page_of_results()
        {
            Assert.IsNull(pageTwoNextPage);
        }

        protected static void Because_of()
        {
            pageOneResponse = client.Post(ServiceUrl["PartyRoleAccountability"] + "search", content);
            var feed1 = LoadFeed(pageOneResponse);
            pageOne = GetPeopleFromFeed(feed1);
            pageOneNextPage = GetNextPageFromFeed(feed1);

            pageTwoResponse = client.Get(pageOneNextPage);
            var feed2 = LoadFeed(pageTwoResponse);
            pageTwo = GetPeopleFromFeed(feed2);
            pageTwoNextPage = GetNextPageFromFeed(feed2);
        }

        protected static void Establish_context()
        {
            entity1 = PartyRoleAccountabilityData.CreateBasicEntity();
            entity2 = PartyRoleAccountabilityData.CreateBasicEntity();

            client = new HttpClient();

			var search = new Search().IsMutliPage().MaxPageSize(1);
            PartyRoleAccountabilityData.CreateSearch(search, entity1, entity2);

            content = HttpContentExtensions.CreateDataContract(RWEST.Nexus.Contracts.Search.SearchExtensions.ToNexus(search));
        }

        private static SyndicationFeed LoadFeed(HttpResponseMessage message)
        {
            XmlReader reader = XmlReader.Create(
                message.Content.ReadAsStream(), new XmlReaderSettings { ProhibitDtd = false });
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            return feed;
        }

        private static IList<PartyRoleAccountability> GetPeopleFromFeed(SyndicationFeed feed)
        {
            List<RWEST.Nexus.MDM.Contracts.PartyRoleAccountability> result =
                feed.Items.Select(syndicationItem => (XmlSyndicationContent)syndicationItem.Content).Select(
                    syndic => syndic.ReadContent<RWEST.Nexus.MDM.Contracts.PartyRoleAccountability>()).ToList();

            return result;
        }

        private static Uri GetNextPageFromFeed(SyndicationFeed feed)
        {
            return feed.Links.Count > 0 ? feed.Links[0].Uri : null;
        }
    }
}

