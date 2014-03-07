namespace MDM.ServiceHost.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Transactions;

    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Services;

    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;
    using MDM.ServiceHost.WebApi.Infrastructure.Feeds;

    using RWEST.Nexus.MDM.Contracts;

    public class EntityFeedController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityFeedController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        public HttpResponseMessage Get()
        {
            List<TContract> list;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
            {
                // TODO: Constrain the identifiers we retrieve or have an enum to allow this e.g. Nexus, Originating, Default, All
                list = new List<TContract>(this.service.List());
                scope.Complete();
            }

            var entityName = typeof(TContract).Name.ToLowerInvariant();

            var feed = new FeedBuilder()
                .WithEntityName(entityName)
                .WithId("list")
                .WithTitle("All")
                .WithItemTitle(entityName)
                .WithItems(list)
                .Build();
            
            return this.Request.CreateResponse(HttpStatusCode.OK, feed, new AtomSyndicationFeedFormatter(), "application/xml");
        }
    }
}
