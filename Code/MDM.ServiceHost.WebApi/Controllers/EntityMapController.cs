using EnergyTrading.Mdm.Messages.Services;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;
using MDM.ServiceHost.WebApi.Infrastructure.Extensions;
using Microsoft.Practices.ServiceLocation;

namespace MDM.ServiceHost.WebApi.Controllers
{
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
    /// This controller handles requests for looking up MDM entities via the mapping functionality.
    /// </summary>
    public class EntityMapController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityMapController(IMdmService<TContract, TEntity> service, IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
            this.service = service;
        }

        /// <summary>
        /// Will try and retrieve an entity with a matching mapping to what is supplied in the query parameters
        /// </summary>
        /// <param name="etag">The service verifies if its entity version matches what's held by the client cache</param>
        /// <returns>Response with appropriate status code and the serialised entity as content if found</returns>
        [ETagChecking]
        public IHttpActionResult Get([IfNoneMatch] ETag etag)
        {
            return WebHandler(() =>
            {
                var request = MessageFactory.MappingRequest(this.QueryParameters);
                request.Version = etag.ToVersion();

                ContractResponse<TContract> response;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = this.service.Map(request);
                    scope.Complete();
                }

                if (response.IsValid)
                {
                    return new ResponseWithETag<TContract>(this.Request, response.Contract, HttpStatusCode.OK,
                        response.Version);
                }

                throw new MdmFaultException(new MappingRequestFaultHandler().Create(typeof(TContract).Name,
                    response.Error, request));
            });
        }
    }
}
