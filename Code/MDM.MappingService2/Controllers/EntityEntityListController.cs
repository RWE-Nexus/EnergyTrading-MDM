using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using EnergyTrading.MDM.MappingService2.Infrastructure.Controllers;
using EnergyTrading.MDM.Messages;
using EnergyTrading.MDM.Services;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.MappingService2.Controllers
{
    public class EntityEntityListController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity 
    {
        protected IMdmService<TContract, TEntity> service;

        public EntityEntityListController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        public IHttpActionResult Get(int id)
        {
            var request = MessageFactory.GetRequest(QueryParameters);
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
                return Ok(resultList);
            }

            // THROW FAULTFACTORY EXCEPTION
            throw new Exception("Undefined exception to be fixed");
        }
    }
}
