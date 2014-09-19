using EnergyTrading.Mdm.Messages.Services;
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

    public class EntityMappingController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityMappingController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        [ETagChecking]
        public IHttpActionResult Get(int id, int mappingid)
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
                return new ResponseWithETag<MappingResponse>(this.Request, response.Contract, HttpStatusCode.OK, response.Version);
            }

            throw new MdmFaultException(new GetMappingRequestFaultHandler().Create(typeof(TContract).Name, response.Error, request));
        }

        public IHttpActionResult Post(int id, [FromBody] EnergyTrading.Mdm.Contracts.Mapping mapping)
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

            return new StatusCodeResultWithLocation(this.Request, HttpStatusCode.Created, location);
        }

        public IHttpActionResult Delete(int id, int mappingid)
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
            // TODO: surely we need to check if the mapping / entity was found ok?
            return this.Ok();
        }

        [HttpPut, HttpPost]
        public IHttpActionResult Put(int id, int mappingid, [IfMatch] ETag etag, [FromBody] Mapping mapping)
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
                return new StatusCodeResultWithLocation(this.Request, HttpStatusCode.NoContent, this.Request.RequestUri.AbsolutePath.Substring(1));
            }

            return this.NotFound();
        }
    }
}
