namespace MDM.ServiceHost.Wcf.Feeds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Syndication;

    using EnergyTrading.Search;
    using EnergyTrading.ServiceModel.Channels;

    public class FeedFactory : IFeedFactory
    {
        private const string MoreResultsUri = "{0}?key={1}&page={2}";

        public Message CreateFeed<TContract>(IList<TContract> contracts, string entityName)
        {
            var formatter = new Atom10FeedFormatter(CreateFeedBody(contracts, entityName));
            return Message.CreateMessage(MessageVersion.None, entityName, new FeedBodyWriter(formatter));
        }

        public Message CreateFeed<TContract>(SearchResultPage<TContract> searchResultPage, string entityName, Uri baseUri)
        {
            var feedBody = CreateFeedBody(searchResultPage.Contracts, entityName);

            if (searchResultPage.NextPage.HasValue)
            {
                var moreresultsuri = string.Format(MoreResultsUri, "search", searchResultPage.SearchResultsKey, searchResultPage.NextPage);
                //TODO: Fix this uri
                feedBody.Links.Add(
                    new SyndicationLink(
                    new Uri(baseUri, moreresultsuri))
                    {
                        RelationshipType = FeedData.NextResults 
                    });
            }

            var formatter = new Atom10FeedFormatter(feedBody);
            return Message.CreateMessage(MessageVersion.None, entityName, new FeedBodyWriter(formatter));
        }

        private static SyndicationFeed CreateFeedBody<TContract>(IList<TContract> contracts, string entityName)
        {
            var feed = new SyndicationFeed();
            feed.Id = string.Format("urn:uuid:{0}:{1}", entityName, "search"); 
            feed.Title = new TextSyndicationContent("Search Results");
            feed.Generator = "Nexus Mapping Service";
            feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
            feed.LastUpdatedTime = EnergyTrading.SystemTime.UtcNow();

            feed.Items = contracts.Select(x => new SyndicationItem
            {
                Title = new TextSyndicationContent(entityName),
                Content = new XmlSyndicationContent("application/xml", new SyndicationElementExtension(x))
            });

            return feed;
        }
    }
}