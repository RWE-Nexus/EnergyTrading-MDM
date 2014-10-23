using System;
using System.Collections.Generic;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Web.Http;
using System.Web.Http.Description;
using EnergyTrading.Mdm.Contracts;
using MDM.ServiceHost.WebApi.Filters;
using MDM.ServiceHost.WebApi.Infrastructure.ETags;

namespace MDM.ServiceHost.WebApi.Infrastructure.ApiDocumentation
{
    /**
     * 
     * NOTE this controller is only here as a way of easily generating a common API help page for all the standard
     * MDM contracts/entities.  Because of the dynamic / reflective nature of the MDM controller mechanism the built-in 
     * WebApi HelpPage library can't auto-generate the api documentation.  The exception is ReferenceData which, because 
     * it isn't a standard entity, has its own controller and thus auto-generated api documentation.
     * 
     * This means that the documentation in this controller needs to be kept up-to-date when anything changes in the real
     * {EntityController} classes.
     * 
     */

    /// <summary>
    /// All the standard MDM entities support the API represented in this section.
    /// For any standard MDM entity replace "mdmentity" in the route with the concrete entity name.
    /// </summary>
    [RoutePrefix("mdmentity")]
    public class MdmEntityController : ApiController
    {
        /// <summary>
        /// Creates a new "MdmEntity" from the details in the request body
        /// </summary>
        /// <param name="contract">The "MdmEntity" contract details</param>
        /// <returns>Reponse with appropriate status code and entity location url</returns>
        [ValidateModel]
        [HttpPost, Route("")]
        public IHttpActionResult CreateEntity([FromBody] MdmEntity contract)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an "MdmEntity" with the details provided in the request body
        /// Supports POST for backwards compatibility, going forward a PUT should be used.
        /// </summary>
        /// <param name="id">The MDM entity identifier</param>
        /// <param name="etag">The version of the entity represented in the request body</param>
        /// <param name="contract">The updated "MdmEntity" details</param>
        /// <returns>Reponse with appropriate status code and entity location url</returns>
        [ValidateModel]
        [AcceptVerbs("POST", "PUT"), Route("{id:int}")]
        public IHttpActionResult UpdateEntity(int id, [IfMatch] ETag etag, [FromBody] MdmEntity contract)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the "MdmEntity" matching the identifier.  If an ETag version is supplied then
        /// the service will verify if it matches its own version and respond accordingly.
        /// </summary>
        /// <param name="id">The MDM unique identifier</param>
        /// <param name="asOfDate">If supplied, the entity validity dates are checked against it.  Format is "yyyy-MM-dd'T'HH:mm:ss.fffffffZ"</param>
        /// <param name="etag">The current version held by the client</param>
        /// <returns>Reponse with appropriate status code and the serialised entity according to content negotiation</returns>
        [HttpGet, Route("{id}")]
        [ResponseType(typeof(MdmEntity))]
        public IHttpActionResult GetEntity(int id, [FromUri(Name = "as-of")] string asOfDate = "", [IfNoneMatch] ETag etag = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of all the instances for this entity type
        /// </summary>
        /// <returns>Reponse with appropriate status code and the list of entities as content</returns>
        [HttpGet, Route("list")]
        [ResponseType(typeof(List<MdmEntity>))]
        public IHttpActionResult GetAllEntities()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cross mapping take a source system name and source system mapping value and looks up
        /// the entity and returns the mapping for the target system.
        /// </summary>
        /// <param name="sourceSystem">The system name that the known mapping belongs to</param>
        /// <param name="mappingValue">The source system mapping value</param>
        /// <param name="destinationSystem">The system to find and return the mapping for</param>
        /// <param name="asOfDate">If supplied, entities are filtered on their validity dates.  Format is "yyyy-MM-dd'T'HH:mm:ss.fffffffZ"</param>
        /// <param name="etag">The current version held by the client</param>
        /// <returns>Response with appropriate status code and the mapping value for the destination system</returns>
        [HttpGet, Route("crossmap")]
        [ResponseType(typeof(Mapping))]
        public IHttpActionResult GetCrossMap([FromUri(Name = "source-system")] string sourceSystem, [FromUri(Name = "mapping-string")] string mappingValue, [FromUri(Name = "destination-system")] string destinationSystem, [FromUri(Name = "as-of")] string asOfDate = "", [IfNoneMatch] ETag etag = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Will try and retrieve a "MdmEntity" with a matching mapping to what is supplied in the query parameters
        /// </summary>
        /// <param name="sourceSystem">The system name that the known mapping belongs to</param>
        /// <param name="mappingValue">The source system mapping value</param>
        /// <param name="asOfDate">If supplied, entities are filtered on their validity dates.  Format is "yyyy-MM-dd'T'HH:mm:ss.fffffffZ"</param>
        /// <param name="etag">The service verifies if its entity version matches what's held by the client cache</param>
        /// <returns>Response with appropriate status code and the serialised entity as content if found</returns>
        [HttpGet, Route("map")]
        [ResponseType(typeof(MdmEntity))]
        public IHttpActionResult MapEntity([FromUri(Name = "source-system")] string sourceSystem, [FromUri(Name = "mapping-string")] string mappingValue, [FromUri(Name = "as-of")] string asOfDate = "", [IfNoneMatch] ETag etag = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the MDM entity mapping that matches the supplied unique identifiers
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the entity mapping</param>
        /// <returns>Reponse with the appropriate status code and the mapping as content</returns>
        [HttpGet, Route("{id}/mapping/{mappingid}")]
        [ResponseType(typeof(Mapping))]
        public IHttpActionResult GetEntityMapping(int id, int mappingid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new entity mapping for the entity specified.
        /// </summary>
        /// <param name="id">The MDM identifier for the entity that the mapping will be attached to</param>
        /// <param name="mapping">The mapping details</param>
        /// <returns>Response with appropriate status code and mapping location url</returns>
        [ValidateModel]
        [HttpPost, Route("{id}/mapping")]
        public IHttpActionResult CreateEntityMapping(int id, [FromBody] Mapping mapping)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity mapping matching the supplied identifiers
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the mapping</param>
        /// <returns>Reponse with appropriate status code</returns>
        [HttpDelete, Route("{id}/mapping/{mappingid}")]
        public IHttpActionResult DeleteEntityMapping(int id, int mappingid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the entity mapping with the details supplied in the request body.
        /// If the client holds an out of date entity (ETag based) then the request fails.
        /// Supports updates via POST for backwards compatibility.
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the mapping</param>
        /// <param name="etag">The version of the entity held by the client</param>
        /// <param name="mapping">The "MdmEntity" contract details</param>
        /// <returns>Response with appropriate status code and the entity mapping location url</returns>
        [ValidateModel]
        [AcceptVerbs("POST", "PUT"), Route("{id}/mapping/{mappingid}")]
        public IHttpActionResult UpdateEntityMapping(int id, int mappingid, [IfMatch] ETag etag, [FromBody] Mapping mapping)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all the mappings for the entity as an Atom XML feed
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="etag">The current version held by the client</param>
        /// <returns>Response with appropriate status code and the Atom feed as content</returns>
        [HttpGet, Route("{id}/mappings")]
        [ResponseType(typeof(SyndicationFeed))]
        public HttpResponseMessage GetMappings(int id, [IfNoneMatch] ETag etag = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of all entity versions for the specified identifier
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="asOfDate">If supplied, entity versions are filtered on their validity dates.  Format is "yyyy-MM-dd'T'HH:mm:ss.fffffffZ"</param>
        /// <returns>Response with appropriate status code and the Atom feed as content</returns>
        [HttpGet, Route("{id}/list")]
        [ResponseType(typeof(SyndicationFeed))]
        public IHttpActionResult GetEntityVersions(int id, [FromUri(Name = "as-of")] string asOfDate = "")
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns a list of the entities as an Atom XML feed
        /// </summary>
        /// <returns>Reponse with appropriate status code and the Atom XML feed as content</returns>
        [HttpGet, Route("feed")]
        [ResponseType(typeof(SyndicationFeed))]
        public HttpResponseMessage GetEntitiesAsFeed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a page of search results for a previously executed search request
        /// </summary>
        /// <param name="key">The unique search key string returned by the initial search request</param>
        /// <param name="page">The page of results to return</param>
        /// <returns>Reponse with appropriate status code and the requested page of search results as content</returns>
        [HttpGet, Route("search")]
        [ResponseType(typeof(SyndicationFeed))]
        public HttpResponseMessage GetSearchResults(string key, int page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initiates a MDM entity search and returns the first page of results as an Atom XML feed
        /// </summary>
        /// <param name="search">The search object containing criteria and search options</param>
        /// <returns>Response with appropriate status code and the first page of results along with links to further result pages</returns>
        [HttpPost, Route("search")]
        [ResponseType(typeof(SyndicationFeed))]
        public HttpResponseMessage PostSearch([FromBody] EnergyTrading.Contracts.Search.Search search)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A fake entity used for api documentation only
    /// </summary>
    public class MdmEntity
    {

    }
}
