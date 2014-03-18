namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Syndication;

    using AutoMapper;

    using EnergyTrading.Search;
    using EnergyTrading.ServiceModel.Channels;

    using global::MDM.ServiceHost.Wcf.Feeds;

    public class LegacyFeedFactory : FeedFactory
    {
        private readonly IMappingEngine mappingEngine;

        private const string MoreResultsUri = "{0}?key={1}&page={2}";

        public LegacyFeedFactory(IMappingEngine mappingEngine)
        {
            this.mappingEngine = mappingEngine;
        }

        public Message CreateFeed<TLegacyContract, TContract>(IList<TContract> contracts, string entityName)
        {
            var formatter = new Atom10FeedFormatter(CreateFeedBody<TLegacyContract, TContract>(contracts, entityName));
            return Message.CreateMessage(MessageVersion.None, entityName, new FeedBodyWriter(formatter));
        }

        public Message CreateFeed<TLegacyContract, TContract>(SearchResultPage<TContract> searchResultPage, string entityName, Uri baseUri)
        {
            var feedBody = CreateFeedBody<TLegacyContract, TContract>(searchResultPage.Contracts, entityName);

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

        private SyndicationFeed CreateFeedBody<TLegacyContract, TContract>(IList<TContract> contracts, string entityName)
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
                Content = new XmlSyndicationContent("application/xml", new SyndicationElementExtension(mappingEngine.Map<TContract, TLegacyContract>(x)))
            });

            return feed;
        }
    }
}