namespace MDM.ServiceHost.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Transactions;
    using System.Web.Http;

    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Services;

    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;

    public class EntityListController<TContract, TEntity, TListContract> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity 
        where TListContract : IList<TContract>, new()
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityListController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        public IHttpActionResult Get()
        {
            var list = new TListContract();

            using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
            {
                foreach (var item in this.service.List())
                {
                    list.Add(item);
                }
                scope.Complete();
            }

            return this.Ok(list);
        }
    }
}
