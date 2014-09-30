using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

namespace MDM.ServiceHost.WebApi.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Search;

    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;
    using MDM.ServiceHost.WebApi.Infrastructure.Feeds;

    using EnergyTrading.Mdm.Contracts;

    public class EntitySearchController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;
        protected const int FirstPage = 1;
        protected string entityName;

        public EntitySearchController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
            this.entityName = typeof(TContract).Name;
        }

        public HttpResponseMessage Get(string key, int page)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new NotFoundException(string.Format("Search results not found for key {0}/{1}", key, page));
            }

            return this.Search(key, page);
        }

        public HttpResponseMessage Post([FromBody] EnergyTrading.Contracts.Search.Search search)
        {
            var key = search.ToKey<TContract>();
            return this.Search(key, FirstPage);
        }

        private HttpResponseMessage Search(string key, int page)
        {
            var searchResults = this.service.GetSearchResults(key, page);
            if (searchResults == null)
            {
                this.service.CreateSearch(key.ToSearch<TContract>());
                searchResults = this.service.GetSearchResults(key, page);
            }
            if (searchResults == null)
            {
                throw new NotFoundException(string.Format("Search results not found for key {0}/{1}", key, page));
            }

            var feedBuilder = new FeedBuilder()
                .WithEntityName(this.entityName)
                .WithId("search")
                .WithTitle("Search Results")
                .WithItemTitle(this.entityName)
                .WithItems(searchResults.Contracts);

            if (searchResults.NextPage.HasValue)
            {
                feedBuilder.AddMoreResultsLink(this.Request.RequestUri, searchResults.SearchResultsKey, searchResults.NextPage);
            }

            var feed = feedBuilder.Build();
            return this.Request.CreateResponse(HttpStatusCode.OK, feed, new AtomSyndicationFeedFormatter(), "application/xml");
        }
    }
}
