using EnergyTrading.Mdm.Messages.Services;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;
using Microsoft.Practices.ServiceLocation;

namespace MDM.ServiceHost.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;
    using System.Web.Http;

    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Mdm.Services;

    using MDM.ServiceHost.WebApi.Infrastructure.Controllers;

    using EnergyTrading.Mdm.Contracts;

    /// <summary>
    /// This controller handles requests relating to lists of MDM entities
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityEntityListController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected IMdmService<TContract, TEntity> service;

        /// <summary>
        /// 
        /// </summary>
        public EntityEntityListController(IMdmService<TContract, TEntity> service, IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
            this.service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MdmFaultException"></exception>
        public IHttpActionResult Get(int id)
        {
            return WebHandler(() =>
            {
                var request = MessageFactory.GetRequest(this.QueryParameters);
                request.EntityId = id;

                IEnumerable<TContract> result;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    result = this.service.EntityList(request);
                    scope.Complete();
                }

                var resultList = result.ToList();

                if (resultList.Any())
                {
                    return this.Ok(resultList);
                }

                throw new MdmFaultException(new GetRequestFaultHandler().Create(typeof(TContract).Name,
                    new ContractError { Reason = ErrorReason.Identifier, Type = ErrorType.NotFound }, request));
            });
        }
    }
}
