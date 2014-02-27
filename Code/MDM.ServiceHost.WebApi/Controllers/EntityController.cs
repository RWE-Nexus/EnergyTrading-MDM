using System;
using System.Net;
using System.Transactions;
using System.Web.Http;
using EnergyTrading.MDM.MappingService2.Filters;
using EnergyTrading.MDM.MappingService2.Infrastructure;
using EnergyTrading.MDM.MappingService2.Infrastructure.Controllers;
using EnergyTrading.MDM.MappingService2.Infrastructure.ETags;
using EnergyTrading.MDM.MappingService2.Infrastructure.Results;
using EnergyTrading.MDM.Messages;
using EnergyTrading.MDM.Services;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.MappingService2.Controllers
{
    public class EntityController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity 
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        [ETagChecking]
        public IHttpActionResult Get(int id, [IfNoneMatch] ETag etag)
        {
            var request = MessageFactory.GetRequest(QueryParameters);
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
                return new ResponseWithETag<TContract>(Request, response.Contract, HttpStatusCode.OK, response.Version);
            }

            // THROW FAULTFACTORY EXCEPTION
            throw new Exception("Undefined exception to be fixed");
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
                Request.RequestUri.AbsolutePath.Substring(1),
                entity.Id,
                QueryConstants.ValidAt,
                entity.Validity.Start.ToString(QueryConstants.DateFormatString));

            return new StatusCodeResultWithLocation(Request, HttpStatusCode.Created, location);
        }

        [HttpPut, HttpPost]
        public IHttpActionResult Put(int id, [IfMatch] ETag etag, [FromBody] TContract contract)
        {
            ContractResponse<TContract> response;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
            {
                var version = etag.ToVersion();
                response = this.service.Update(id, version, contract);
                scope.Complete();
            }

            if (response.Contract != null)
            {
                return new StatusCodeResultWithLocation(Request, HttpStatusCode.NoContent, Request.RequestUri.AbsolutePath.Substring(1));
            }

            return NotFound();
        }

        public IHttpActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
