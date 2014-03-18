namespace MDM.ServiceHost.WebApi.Controllers
{
    using System;
    using System.Net;
    using System.Transactions;
    using System.Web.Http;

    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    using MDM.ServiceHost.WebApi.Filters;
    using MDM.ServiceHost.WebApi.Infrastructure;
    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;
    using MDM.ServiceHost.WebApi.Infrastructure.ETags;
    using MDM.ServiceHost.WebApi.Infrastructure.Results;

    using EnergyTrading.Mdm.Contracts;

    public class EntityMapController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityMapController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        [ETagChecking]
        public IHttpActionResult Get([IfNoneMatch] ETag etag)
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
                return new ResponseWithETag<TContract>(this.Request, response.Contract, HttpStatusCode.OK, response.Version);
            }
            
            // THROW FAULTFACTORY EXCEPTION
            throw new Exception("Undefined exception to be fixed");
        }
    }
}
