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

    using Filters;
    using Infrastructure.Controllers;
    using Infrastructure.ETags;
    using Infrastructure.Results;

    using EnergyTrading.Mdm.Contracts;

    /// <summary>
    /// This controller handles the basic CRU(D) operations at the MDM entity level.
    /// </summary>
    public class EntityController<TContract, TEntity> : BaseEntityController<TContract, TEntity>
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        private readonly IMdmNotificationService notificationService;

        /// <summary>
        /// Requires the underlying entity specific service as well as a notification service used for sending notifications of
        /// entity changes.
        /// </summary>
        public EntityController(IMdmService<TContract, TEntity> service, IMdmNotificationService notificationService, IServiceLocator serviceLocator)
            : base(service, serviceLocator)
        {
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Returns the MDM entity matching the identifier.  If an ETag version is supplied then
        /// the service will verify if it matches its own version and respond accordingly.
        /// </summary>
        /// <param name="id">The MDM unique identifier</param>
        /// <param name="etag">The current version held by the client</param>
        /// <returns>Reponse with appropriate status code and the serialised entity according to content negotiation</returns>
        [ETagChecking]
        public IHttpActionResult Get(int id, [IfNoneMatch] ETag etag)
        {
            return WebHandler(() =>
            {
                var request = MessageFactory.GetRequest(QueryParameters);
                request.EntityId = id;
                request.Version = etag.ToVersion();

                ContractResponse<TContract> response;

                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = service.Request(request);
                    scope.Complete();
                }

                if (response.IsValid)
                {
                    return new ResponseWithETag<TContract>(Request, response.Contract, HttpStatusCode.OK,
                        response.Version);
                }

                throw new MdmFaultException(new GetRequestFaultHandler().Create(typeof(TContract).Name, response.Error,
                    request));
            });
        }

        /// <summary>
        /// Creates a new entity from the details in the request body
        /// </summary>
        /// <param name="contract">The deserialised entity details from the request body</param>
        /// <returns>Reponse with appropriate status code and entity url</returns>
        [ValidateModel]
        public IHttpActionResult Post([FromBody] TContract contract)
        {
            return WebHandler(() =>
            {
                TEntity entity;

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    entity = service.Create(contract);
                    scope.Complete();
                }

                var location = String.Format("{0}/{1}?{2}={3}",
                    Request.RequestUri.AbsolutePath.Substring(1),
                    entity.Id,
                    QueryConstants.ValidAt,
                    entity.Validity.Start.ToString(QueryConstants.DateFormatString));

                notificationService.Notify(() => GetContract(entity.Id).Contract, service.ContractVersion,
                    Operation.Created);

                return new StatusCodeResultWithLocation(Request, HttpStatusCode.Created, location);
            });
        }

        /// <summary>
        /// Updates a MDM entity with the details provided in the request body
        /// </summary>
        /// <param name="id">The MDM entity identifier</param>
        /// <param name="etag">The version of the entity represented in the request body</param>
        /// <param name="contract">The updated entity details</param>
        /// <returns>Reponse with appropriate status code and entity url</returns>
        [ValidateModel]
        [HttpPut, HttpPost]
        public IHttpActionResult Put(int id, [IfMatch] ETag etag, [FromBody] TContract contract)
        {
            return WebHandler(() =>
            {
                ContractResponse<TContract> response;

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    var version = etag.ToVersion();
                    response = service.Update(id, version, contract);
                    scope.Complete();
                }

                if (response.Contract != null)
                {
                    notificationService.Notify(() => response.Contract, service.ContractVersion, Operation.Modified);
                    return new StatusCodeResultWithLocation(Request, HttpStatusCode.NoContent,
                        Request.RequestUri.AbsolutePath.Substring(1));
                }

                return NotFound();
            });
        }

        /// <summary>
        /// Deletion of MDM entities is not supported, they should instead have their validity dates altered
        /// </summary>
        public IHttpActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
