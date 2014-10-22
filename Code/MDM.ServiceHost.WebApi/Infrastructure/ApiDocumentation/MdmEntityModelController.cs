using System;
using System.Net.Http;
using System.Web.Http;
using EnergyTrading.Mdm.Contracts;
using MDM.ServiceHost.WebApi.Filters;
using MDM.ServiceHost.WebApi.Infrastructure.ETags;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

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
    /// This fake "MdmEntityModel" API represents what is supported by all the standard MDM entities.
    /// For any standard MDM entity replace "mdmentitymodel" in the route with the concrete entity name.
    /// NOTE: Any entities with a version higher than 1 the route begins with ~/v{version number}/{mdmentitymodel}/...
    /// </summary>
    [RoutePrefix("mdmentitymodel")]
    public class MdmEntityModelController : ApiController
    {
        /// <summary>
        /// Returns the MDM entity matching the identifier.  If an ETag version is supplied then
        /// the service will verify if it matches its own version and respond accordingly.
        /// </summary>
        /// <param name="id">The MDM unique identifier</param>
        /// <param name="etag">The current version held by the client</param>
        /// <returns>Reponse with appropriate status code and the serialised entity according to content negotiation</returns>
        [ETagChecking]
        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(int id, [IfNoneMatch] ETag etag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of all the instances for this entity type
        /// </summary>
        /// <returns>Reponse with appropriate status code and the list of entities as content</returns>
        [HttpGet, Route("list")]
        public IHttpActionResult Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cross mapping take a source system name and source system mapping value and looks up
        /// the entity and returns the mapping for the target system.
        /// </summary>
        /// <param name="etag">The current version held by the client</param>
        /// <returns></returns>
        [ETagChecking]
        [HttpGet, Route("crossmap")]
        public IHttpActionResult GetCrossMap([IfNoneMatch] ETag etag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Will try and retrieve an entity with a matching mapping to what is supplied in the query parameters
        /// </summary>
        /// <param name="etag">The service verifies if its entity version matches what's held by the client cache</param>
        /// <returns>Response with appropriate status code and the serialised entity as content if found</returns>
        [ETagChecking]
        [HttpGet, Route("map")]
        public IHttpActionResult Get([IfNoneMatch] ETag etag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the MDM entity mapping that matches the supplied unique identifiers
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the entity mapping</param>
        /// <returns>Reponse with the appropriate status code and the mapping as content</returns>
        [ETagChecking]
        [HttpGet, Route("{id}/mapping/{mappingid}")]
        public IHttpActionResult Get(int id, int mappingid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new entity mapping for the entity specified.
        /// </summary>
        /// <param name="id">The MDM identifier for the entity that the mapping will be attached to</param>
        /// <param name="mapping">The mapping details deserialised from the request body</param>
        /// <returns>Response with appropriate status code and mapping url</returns>
        [ValidateModel]
        [HttpPost, Route("{id}/mapping")]
        public IHttpActionResult Post(int id, [FromBody] Mapping mapping)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the entity mapping matching the supplied identifiers
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the mapping</param>
        /// <returns>Reponse with approprtiate status code</returns>
        [HttpDelete, Route("{id}/mapping/{mappingid}")]
        public IHttpActionResult Delete(int id, int mappingid)
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
        /// <param name="mapping">The deserialised entity from the request body</param>
        /// <returns>Response with appropriate status code and the entity mapping url</returns>
        [ValidateModel]
        [AcceptVerbs("POST", "PUT"), Route("{id}/mapping/{mappingid}")]
        public IHttpActionResult Put(int id, int mappingid, [IfMatch] ETag etag, [FromBody] Mapping mapping)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all the mappings for the entity as an Atom XML feed
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="etag">The current version held by the client</param>
        /// <returns>Response with approrpiate status code and the Atom feed as content</returns>
        [HttpGet, Route("{id}/mappings")]
        public HttpResponseMessage GetMappings(int id, [IfNoneMatch] ETag etag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of all entity versions for the specified identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MdmFaultException"></exception>
        [HttpGet, Route("{id}/list")]
        public IHttpActionResult Get(int id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns a list of the entities as an Atom XML feed
        /// </summary>
        /// <returns>Reponse with appropriate status code and the Atom XML feed as content</returns>
        [HttpGet, Route("feed")]
        public HttpResponseMessage GetFeed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new entity from the details in the request body
        /// </summary>
        /// <param name="contract">The deserialised entity details from the request body</param>
        /// <returns>Reponse with appropriate status code and entity url</returns>
        [ValidateModel]
        [HttpPost, Route("search")]
        public IHttpActionResult Post([FromBody] MdmEntityModel contract)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a MDM entity with the details provided in the request body
        /// Exists as a POST for backwards compatibility, please use PUT
        /// </summary>
        /// <param name="id">The MDM entity identifier</param>
        /// <param name="etag">The version of the entity represented in the request body</param>
        /// <param name="contract">The updated entity details</param>
        /// <returns>Reponse with appropriate status code and entity url</returns>
        [ValidateModel]
        [AcceptVerbs("POST", "PUT"), Route("{id:int}")]
        public IHttpActionResult Put(int id, [IfMatch] ETag etag, [FromBody] MdmEntityModel contract)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a page of search results for a previously executed search request
        /// </summary>
        /// <param name="key">The unique search key string returned by the initial search request</param>
        /// <param name="page">The page of results to return</param>
        /// <returns>Reponse with approprtiate status code and the page of search results as content</returns>
        [HttpGet, Route("search")]
        public HttpResponseMessage Get(string key, int page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initiates a MDM entity search and returns the first page of results as an Atom XML feed
        /// </summary>
        /// <param name="search">The deserialised search object from the request body</param>
        /// <returns>Response with appropriate status code and the first page of results along with links to further result pages</returns>
        [HttpPost, Route("search")]
        public HttpResponseMessage Post([FromBody] EnergyTrading.Contracts.Search.Search search)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A fake entity used for api documentation only
    /// </summary>
    public class MdmEntityModel
    {

    }
}
