using EnergyTrading.Mdm.Messages.Services;
using EnergyTrading.Mdm.Notifications;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;
using MDM.ServiceHost.WebApi.Infrastructure.Extensions;
using Microsoft.Practices.ServiceLocation;

namespace MDM.ServiceHost.WebApi.Controllers
{
    using System;
    using System.Net;
    using System.Transactions;
    using System.Web.Http;

    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Mdm.Services;

    using MDM.ServiceHost.WebApi.Filters;
    using MDM.ServiceHost.WebApi.Infrastructure;
    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;
    using MDM.ServiceHost.WebApi.Infrastructure.ETags;
    using MDM.ServiceHost.WebApi.Infrastructure.Results;

    using EnergyTrading.Mdm.Contracts;

    /// <summary>
    /// This controller handles requests for the maintenance of MDM entity mappings
    /// </summary>
    public class EntityMappingController<TContract, TEntity> : BaseEntityController<TContract, TEntity>
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        private readonly IMdmNotificationService notificationService;

        /// <summary>
        /// 
        /// </summary>
        public EntityMappingController(IMdmService<TContract, TEntity> service, IMdmNotificationService notificationService, IServiceLocator serviceLocator)
            : base(service, serviceLocator)
        {
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Returns the MDM entity mapping that matches the supplied unique identifiers
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the entity mapping</param>
        /// <returns>Reponse with the appropriate status code and the mapping as content</returns>
        [ETagChecking]
        public IHttpActionResult Get(int id, int mappingid)
        {
            return WebHandler(() =>
            {
                var request = new GetMappingRequest
                {
                    EntityId = id,
                    MappingId = mappingid
                };

                ContractResponse<MappingResponse> response;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = this.service.RequestMapping(request);
                    scope.Complete();
                }

                if (response.IsValid)
                {
                    return new ResponseWithETag<MappingResponse>(this.Request, response.Contract, HttpStatusCode.OK,
                        response.Version);
                }

                throw new MdmFaultException(new GetMappingRequestFaultHandler().Create(typeof(TContract).Name,
                    response.Error, request));
            });
        }

        /// <summary>
        /// Creates a new entity mapping for the entity specified.
        /// </summary>
        /// <param name="id">The MDM identifier for the entity that the mapping will be attached to</param>
        /// <param name="mapping">The mapping details deserialised from the request body</param>
        /// <returns>Response with appropriate status code and mapping url</returns>
        [ValidateModel]
        public IHttpActionResult Post(int id, [FromBody] Mapping mapping)
        {
            return WebHandler(() =>
            {
                var request = new CreateMappingRequest
                {
                    EntityId = id,
                    Mapping = mapping
                };

                IEntityMapping entityMapping;

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    entityMapping = this.service.CreateMapping(request);
                    scope.Complete();
                }

                var location = String.Format("{0}/{1}",
                    this.Request.RequestUri.AbsolutePath.Substring(1),
                    entityMapping.Id);

                notificationService.Notify(() => GetContract(id).Contract, service.ContractVersion, Operation.Modified);

                return new StatusCodeResultWithLocation(this.Request, HttpStatusCode.Created, location);
            });
        }

        /// <summary>
        /// Deletes the entity mapping matching the supplied identifiers
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the mapping</param>
        /// <returns>Reponse with approprtiate status code</returns>
        public IHttpActionResult Delete(int id, int mappingid)
        {
            return WebHandler(() =>
            {
                var request = new DeleteMappingRequest
                {
                    MappingId = mappingid
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    this.service.DeleteMapping(request);
                    scope.Complete();
                }

                notificationService.Notify(() => GetContract(id).Contract, service.ContractVersion, Operation.Modified);
                return Ok();
            });
        }

        /// <summary>
        /// Updates the entity mapping with the details supplied in the request body.
        /// If the client holds an out of date entity (ETag based) then the request fails.
        /// </summary>
        /// <param name="id">The MDM identifier for the entity</param>
        /// <param name="mappingid">The MDM identifier for the mapping</param>
        /// <param name="etag">The version of the entity held by the client</param>
        /// <param name="mapping">The deserialised entity from the request body</param>
        /// <returns>Response with appropriate status code and the entity mapping url</returns>
        [ValidateModel]
        [HttpPut, HttpPost]
        public IHttpActionResult Put(int id, int mappingid, [IfMatch] ETag etag, [FromBody] Mapping mapping)
        {
            return WebHandler(() =>
            {
                IEntityMapping returnedMapping = null;

                var request = new AmendMappingRequest
                {
                    EntityId = id,
                    MappingId = mappingid,
                    Mapping = mapping,
                    Version = etag.ToVersion()
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    returnedMapping = this.service.UpdateMapping(request);
                    scope.Complete();
                }

                if (returnedMapping != null)
                {
                    notificationService.Notify(() => GetContract(id, etag.ToVersion()).Contract, service.ContractVersion,
                        Operation.Modified);
                    return new StatusCodeResultWithLocation(this.Request, HttpStatusCode.NoContent,
                        this.Request.RequestUri.AbsolutePath.Substring(1));
                }

                return NotFound();
            });
        }
    }
}
