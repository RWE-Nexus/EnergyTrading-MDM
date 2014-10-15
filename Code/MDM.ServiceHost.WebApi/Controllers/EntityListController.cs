using Microsoft.Practices.ServiceLocation;

namespace MDM.ServiceHost.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Transactions;
    using System.Web.Http;

    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Services;

    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;

    /// <summary>
    /// This controller handles requests for returning lists of the MDM entity
    /// </summary>
    public class EntityListController<TContract, TEntity, TListContract> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
        where TListContract : IList<TContract>, new()
    {
        protected IMdmService<TContract, TEntity> service;

        /// <summary>
        /// 
        /// </summary>
        public EntityListController(IMdmService<TContract, TEntity> service, IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns a list of all the instances for this entity type
        /// </summary>
        /// <returns>Reponse with appropriate status code and the list of entities as content</returns>
        public IHttpActionResult Get()
        {
            return WebHandler(() =>
            {
                var list = new TListContract();

                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    foreach (var item in service.List())
                    {
                        list.Add(item);
                    }
                    scope.Complete();
                }

                return Ok(list);
            });
        }
    }
}
