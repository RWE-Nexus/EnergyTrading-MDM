using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace EnergyTrading.MDM.MappingService2.Infrastructure.Feeds
{
    public class FeedBuilder
    {
        private string entityName;
        private string id;
        private string title;
        private string itemTitle;
        private IEnumerable items;
        private SyndicationLink moreResultsLink;

        public FeedBuilder()
        {
            items = new List<object>();
        }

        public FeedBuilder WithEntityName(string entityName)
        {
            this.entityName = entityName;
            return this;
        }

        public FeedBuilder WithId(string id)
        {
            this.id = id;
            return this;
        }

        public FeedBuilder WithTitle(string title)
        {
            this.title = title;
            return this;
        }

        public FeedBuilder WithItemTitle(string itemTitle)
        {
            this.itemTitle = itemTitle;
            return this;
        }

        public FeedBuilder WithItems(IEnumerable items)
        {
            this.items = items;
            return this;
        }

        public FeedBuilder AddMoreResultsLink(Uri baseUri, string key, int? page)
        {
            var moreresultsuri = string.Format("{0}?key={1}&page={2}", "search", key, page);
            
            //TODO: Fix this uri

            moreResultsLink = new SyndicationLink(new Uri(baseUri, moreresultsuri))
                {
                    RelationshipType = "next-results"
                };

            return this;
        }

        public SyndicationFeed Build()
        {
            var feed = new SyndicationFeed();
            feed.Id = string.Format("urn:uuid:{0}:{1}", entityName, id);
            feed.Title = new TextSyndicationContent(title);
            feed.Generator = "Nexus Mapping Service";
            feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
            feed.LastUpdatedTime = SystemTime.UtcNow();

            feed.Items = (from object item in items
                          select new SyndicationItem()
                            {
                                Title = new TextSyndicationContent(itemTitle),
                                Content = new XmlSyndicationContent("application/xml", new SyndicationElementExtension(item))
                            }).ToList();

            if (moreResultsLink != null)
            {
                feed.Links.Add(moreResultsLink);
            }

            return feed;
        }
    }
}
