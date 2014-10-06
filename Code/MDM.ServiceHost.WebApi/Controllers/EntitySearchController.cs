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

    /// <summary>
    /// This controller handles requests for performing MDM entity searches
    /// </summary>
    public class EntitySearchController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;
        protected const int FirstPage = 1;
        protected string entityName;

        /// <summary>
        /// 
        /// </summary>
        public EntitySearchController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
            this.entityName = typeof(TContract).Name;
        }

        /// <summary>
        /// Retrieves a page of search results for a previously executed search request
        /// </summary>
        /// <param name="key">The unique search key string returned by the initial search request</param>
        /// <param name="page">The page of results to return</param>
        /// <returns>Reponse with approprtiate status code and the page of search results as content</returns>
        public HttpResponseMessage Get(string key, int page)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new NotFoundException(string.Format("Search results not found for key {0}/{1}", key, page));
            }

            return this.Search(key, page);
        }

        /// <summary>
        /// Initiates a MDM entity search and returns the first page of results as an Atom XML feed
        /// </summary>
        /// <param name="search">The deserialised search object from the request body</param>
        /// <returns>Response with appropriate status code and the first page of results along with links to further result pages</returns>
        public HttpResponseMessage Post([FromBody] EnergyTrading.Contracts.Search.Search search)
        {
            var key = search.ToKey<TContract>(service.ContractVersion);
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
