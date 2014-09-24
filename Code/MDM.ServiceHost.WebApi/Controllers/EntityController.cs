using EnergyTrading.Mdm.Messages.Services;
using EnergyTrading.Mdm.Notifications;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

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

    public class EntityController<TContract, TEntity> : BaseEntityController<TContract, TEntity>
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        private readonly IMdmNotificationService notificationService;

        public EntityController(IMdmService<TContract, TEntity> service, IMdmNotificationService notificationService)
            : base(service)
        {
            this.notificationService = notificationService;
        }

        [ETagChecking]
        public IHttpActionResult Get(int id, [IfNoneMatch] ETag etag)
        {
            var request = MessageFactory.GetRequest(this.QueryParameters);
            request.EntityId = id;
            request.Version = etag.ToVersion();

            ContractResponse<TContract> response;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
            {
                response = this.service.Request(request);
                scope.Complete();
            }

            if (response.IsValid)
            {
                return new ResponseWithETag<TContract>(this.Request, response.Contract, HttpStatusCode.OK, response.Version);
            }

            throw new MdmFaultException(new GetRequestFaultHandler().Create(typeof(TContract).Name, response.Error, request));
        }

        public IHttpActionResult Post([FromBody] TContract contract)
        {
            TEntity entity;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
            {
                entity = this.service.Create(contract);
                scope.Complete();
            }

            var location = String.Format("{0}/{1}?{2}={3}",
                this.Request.RequestUri.AbsolutePath.Substring(1),
                entity.Id,
                QueryConstants.ValidAt,
                entity.Validity.Start.ToString(QueryConstants.DateFormatString));

            notificationService.Notify(() => GetContract(entity.Id).Contract, service.ContractVersion, Operation.Created);

            return new StatusCodeResultWithLocation(this.Request, HttpStatusCode.Created, location);
        }

        [HttpPut, HttpPost]
        public IHttpActionResult Put(int id, [IfMatch] ETag etag, [FromBody] TContract contract)
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
                return new StatusCodeResultWithLocation(this.Request, HttpStatusCode.NoContent, this.Request.RequestUri.AbsolutePath.Substring(1));
            }

            return NotFound();
        }

        public IHttpActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
