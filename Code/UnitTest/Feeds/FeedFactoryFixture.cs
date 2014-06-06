namespace EnergyTrading.Mdm.Test.Feeds
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Syndication;

    using EnergyTrading.Search;
    using EnergyTrading.Test;

    using global::Mdm.ServiceHost.Wcf.Feeds;

    using NUnit.Framework;

    [TestFixture]
    public class when_the_feed_factory_is_asked_to_create_a_feed_and_there_are_multiple_pages : SpecBase<FeedFactory>
    {
        private Message response;
        private Guid currentSearch = Guid.NewGuid();
        private Uri baseUri = new Uri("http://blah/");

        protected override FeedFactory Establish_context()
        {
            return new FeedFactory();
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

            this.response = this.Sut.CreateFeed(searchResultPage, "SourceSystem", this.baseUri);
        }

        [Test] 
        public void should_include_the_next_page_of_search_results_as_a_relative_link()
        {
            var formatter = new Atom10FeedFormatter();
            formatter.ReadFrom(this.response.GetReaderAtBodyContents());
            var links = formatter.Feed.Links;
            
            Assert.AreEqual(this.baseUri + "search?key=" + this.currentSearch + "&page=2", links[0].Uri.ToString());
        }

        [Test]
        public void should_set_the_correct_name_for_the_next_search_results_link()
        {
            var formatter = new Atom10FeedFormatter();
            formatter.ReadFrom(this.response.GetReaderAtBodyContents());
            var links = formatter.Feed.Links;
            Assert.AreEqual(FeedData.NextResults, links[0].RelationshipType);
        }
    }

    [TestFixture]
    public class when_the_feed_factory_is_asked_to_create_a_feed_and_there_are_multiple_pages_but_this_is_the_last_page : SpecBase<FeedFactory>
    {
        private Message response;
        private Guid currentSearch = Guid.NewGuid();
        private Uri baseUri = new Uri("http://blah/");

        protected override FeedFactory Establish_context()
        {
            return new FeedFactory();
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

            this.response = this.Sut.CreateFeed(searchResultPage, "SourceSystem", this.baseUri);
        }

        [Test] 
        public void should_not_include_a_link_for_the_next_page()
        {
            var formatter = new Atom10FeedFormatter();
            formatter.ReadFrom(this.response.GetReaderAtBodyContents());
            var links = formatter.Feed.Links;
            
            Assert.AreEqual(0, links.Count);
        }
    }
}