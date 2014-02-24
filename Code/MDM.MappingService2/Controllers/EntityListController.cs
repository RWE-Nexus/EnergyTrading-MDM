using System.Collections.Generic;
using System.Transactions;
using System.Web.Http;
using EnergyTrading.MDM.MappingService2.Infrastructure.Controllers;
using EnergyTrading.MDM.Services;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.MappingService2.Controllers
{
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

            return Ok(list);
        }
    }
}
