namespace MDM.ServiceHost.WebApi.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Transactions;

    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    using MDM.ServiceHost.WebApi.Infrastructure;
    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;
    using MDM.ServiceHost.WebApi.Infrastructure.ETags;
    using MDM.ServiceHost.WebApi.Infrastructure.Feeds;

    using EnergyTrading.Mdm.Contracts;

    public class EntityMappingsController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityMappingsController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        public HttpResponseMessage Get(int id, [IfNoneMatch] ETag etag)
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

            if (!response.IsValid)
            {
                // THROW FAULTFACTORY EXCEPTION
                throw new Exception("Undefined exception to be fixed");
            }

            var entityName = typeof(TContract).Name.ToLowerInvariant();
            var title = string.Format("Mappings for {0} {1} ", entityName, id);
            var feed = new FeedBuilder()
                .WithEntityName(entityName)
                .WithId(id.ToString())
                .WithTitle(title)
                .WithItemTitle("mapping")
                .WithItems(response.Contract.Identifiers)
                .Build();

            return this.Request.CreateResponse(HttpStatusCode.OK, feed, new AtomSyndicationFeedFormatter(), "application/xml");
        }
    }
}
