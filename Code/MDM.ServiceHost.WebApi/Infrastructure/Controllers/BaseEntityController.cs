using System;
using System.Configuration;
using EnergyTrading.Mdm;
using EnergyTrading.Mdm.Contracts;
using EnergyTrading.Mdm.Messages;
using EnergyTrading.Mdm.Messages.Services;
using EnergyTrading.Mdm.Services;
using MDM.ServiceHost.WebApi.Infrastructure.Exceptions;

namespace MDM.ServiceHost.WebApi.Infrastructure.Controllers
{
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Transactions;
    using System.Web.Http;

    public class BaseEntityController<TContract, TEntity> : BaseEntityController
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        protected readonly IMdmService<TContract, TEntity> service;

        public BaseEntityController(IMdmService<TContract, TEntity> service)
        {
            this.service = service;
        }

        protected ContractResponse<TContract> GetContract(int id, ulong etagVersion = 0UL)
        {
            var request = MessageFactory.GetRequest(QueryParameters);
            request.EntityId = id;
            request.Version = etagVersion;

            ContractResponse<TContract> response;
            using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
            {
                response = service.Request(request);
                scope.Complete();
            }

            if (!response.IsValid)
            {
                throw new MdmFaultException(new GetRequestFaultHandler().Create(typeof(TContract).Name, response.Error, request));
            }

            return response;
        }
    }

    public class BaseEntityController : ApiController
    {

        /// <summary>
        /// Standard transaction options for reading data
        /// </summary>
        /// <returns></returns>
        protected static TransactionOptions ReadOptions()
        {
            return new TransactionOptions { IsolationLevel = ReadIsolation };
        }

        /// <summary>
        /// Standard transactions options for writing data
        /// </summary>
        /// <returns></returns>
        protected static TransactionOptions WriteOptions()
        {
            return new TransactionOptions { IsolationLevel = WriteIsolation };
        }

        private static IsolationLevel ReadIsolation
        {
            get { return ConfigIsolation("Mdm.ReadIsolation"); }
        }

        private static IsolationLevel WriteIsolation
        {
            get { return ConfigIsolation("Mdm.WriteIsolation"); }
        }

        protected NameValueCollection QueryParameters
        {
            // HACK
            get { return this.Request.RequestUri.ParseQueryString(); }
        }

        private static IsolationLevel ConfigIsolation(string name, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var value = ConfigurationManager.AppSettings[name];
            IsolationLevel iso;
            return Enum.TryParse(value, out iso) ? iso : isolationLevel;
        }
    }
}
