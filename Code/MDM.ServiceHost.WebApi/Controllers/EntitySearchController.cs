using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EnergyTrading.MDM.Extensions;
using EnergyTrading.MDM.MappingService2.Infrastructure;
using EnergyTrading.MDM.MappingService2.Infrastructure.Controllers;
using EnergyTrading.MDM.MappingService2.Infrastructure.Feeds;
using EnergyTrading.MDM.Services;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.MappingService2.Controllers
{
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
                // THROW FAULTFACTORY EXCEPTION
                throw new Exception("Undefined exception to be fixed");
            }

            return Search(key, FirstPage);
        }

        public HttpResponseMessage Post([FromBody] EnergyTrading.Contracts.Search.Search search)
        {
            var key = search.ToKey<TContract>();
            return Search(key, FirstPage);
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
                // THROW FAULTFACTORY EXCEPTION
                throw new Exception("Undefined exception to be fixed");
            }

            var feedBuilder = new FeedBuilder()
                .WithEntityName(entityName)
                .WithId("search")
                .WithTitle("Search Results")
                .WithItemTitle(entityName)
                .WithItems(searchResults.Contracts);

            if (searchResults.NextPage.HasValue)
            {
                feedBuilder.AddMoreResultsLink(Request.RequestUri, searchResults.SearchResultsKey, searchResults.NextPage);
            }
            
            var feed = feedBuilder.Build();
            return Request.CreateResponse(HttpStatusCode.OK, feed, new AtomSyndicationFeedFormatter(), "application/xml");
        }
    }
}
