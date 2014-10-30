using MDM.ServiceHost.WebApi.Infrastructure.Feeds;

namespace EnergyTrading.Mdm.Test.Feeds
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;

    using EnergyTrading.Search;
    using EnergyTrading.Test;

    using NUnit.Framework;

    [TestFixture]
    public class when_the_feed_factory_is_asked_to_create_a_feed_and_there_are_multiple_pages : SpecBase<FeedBuilder>
    {
        private SyndicationFeed response;
        private Guid currentSearch = Guid.NewGuid();
        private Uri baseUri = new Uri("http://blah/");

        protected override FeedBuilder Establish_context()
        {
            return new FeedBuilder();
        }

        protected override void Because_of()
        {
            var bobSmith = new EnergyTrading.Mdm.Contracts.SourceSystem()
                {
                    Details = new EnergyTrading.Mdm.Contracts.SourceSystemDetails() { Name = "Bob" } 
                };

            var fredJones = new EnergyTrading.Mdm.Contracts.SourceSystem()
                {
                    Details = new EnergyTrading.Mdm.Contracts.SourceSystemDetails() { Name = "Fred" } 
                };

            var personList = new List<EnergyTrading.Mdm.Contracts.SourceSystem>() { bobSmith, fredJones };

            var cacheSearchResultPage = new CacheSearchResultPage(new List<int>() { 1, 2 }, DateTime.Now, 2, this.currentSearch.ToString());
            var searchResultPage = new SearchResultPage<EnergyTrading.Mdm.Contracts.SourceSystem>(cacheSearchResultPage, personList);

            this.response = this.Sut.WithEntityName("SourceSystem").WithItems(searchResultPage.Contracts).AddMoreResultsLink(this.baseUri, searchResultPage.SearchResultsKey, searchResultPage.NextPage).Build();
        }

        [Test] 
        public void should_include_the_next_page_of_search_results_as_a_relative_link()
        {
            var formatter = new Atom10FeedFormatter(response);
            var links = formatter.Feed.Links;
            
            Assert.AreEqual(this.baseUri + "search?key=" + this.currentSearch + "&page=2", links[0].Uri.ToString());
        }

        [Test]
        public void should_set_the_correct_name_for_the_next_search_results_link()
        {
            var formatter = new Atom10FeedFormatter(response);
            var links = formatter.Feed.Links;
            Assert.AreEqual("next-results", links[0].RelationshipType);
        }
    }

    [TestFixture]
    public class when_the_feed_factory_is_asked_to_create_a_feed_and_there_are_multiple_pages_but_this_is_the_last_page : SpecBase<FeedBuilder>
    {
        private SyndicationFeed response;
        private Guid currentSearch = Guid.NewGuid();
        private Uri baseUri = new Uri("http://blah/");

        protected override FeedBuilder Establish_context()
        {
            return new FeedBuilder();
        }

        protected override void Because_of()
        {
            var bobSmith = new EnergyTrading.Mdm.Contracts.SourceSystem()
                {
                    Details = new EnergyTrading.Mdm.Contracts.SourceSystemDetails() { Name = "Bob" } 
                };

            var fredJones = new EnergyTrading.Mdm.Contracts.SourceSystem()
                {
                    Details = new EnergyTrading.Mdm.Contracts.SourceSystemDetails() { Name = "Fred" } 
                };

            var personList = new List<EnergyTrading.Mdm.Contracts.SourceSystem>() { bobSmith, fredJones };

            var cacheSearchResultPage = new CacheSearchResultPage(new List<int>() { 1, 2 }, DateTime.Now, null, this.currentSearch.ToString());
            var searchResultPage = new SearchResultPage<EnergyTrading.Mdm.Contracts.SourceSystem>(cacheSearchResultPage, personList);

            this.response = this.Sut.WithEntityName("SourceSystem").WithItems(searchResultPage.Contracts).Build();
        }

        [Test] 
        public void should_not_include_a_link_for_the_next_page()
        {
            var formatter = new Atom10FeedFormatter(response);
            var links = formatter.Feed.Links;
            
            Assert.AreEqual(0, links.Count);
        }
    }
}