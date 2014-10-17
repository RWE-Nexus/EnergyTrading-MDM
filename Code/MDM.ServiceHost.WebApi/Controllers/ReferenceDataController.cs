using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Transactions;
using System.Web.Http;
using EnergyTrading.Data;
using EnergyTrading.Data.EntityFramework;
using EnergyTrading.Extensions;
using EnergyTrading.Logging;
using EnergyTrading.Mdm.Contracts;
using MDM.ServiceHost.WebApi.Infrastructure.Results;
using Microsoft.Practices.ServiceLocation;
using ReferenceData = EnergyTrading.Mdm.ReferenceData;

namespace MDM.ServiceHost.WebApi.Controllers
{
    /// <summary>
    /// Specific controller for the ReferenceData MDM type which isn't a proper entity or contract
    /// </summary>
    [RoutePrefix("referencedata")]
    public class ReferenceDataController : ApiController
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRepository repository;
        private readonly IServiceLocator serviceLocator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="serviceLocator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ReferenceDataController(IRepository repository, IServiceLocator serviceLocator)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            this.repository = repository;
            this.serviceLocator = serviceLocator;
        }

        [HttpGet, Route("list")]
        public IHttpActionResult List()
        {
            return GetHandler(
                "{}",
                () =>
                {
                    var entries = repository.Queryable<ReferenceData>().OrderBy(x => x.Key + "|" + x.Value);

                    var list = new ReferenceDataList();
                    list.AddRange(entries.Select(entry => new EnergyTrading.Mdm.Contracts.ReferenceData { ReferenceKey = entry.Key, Value = entry.Value }));
                    Logger.DebugFormat("ReferenceData list created. Key: {0}", "{}");
                    return Ok(list);
                });
        }

        [HttpGet, Route("list/{key}")]
        public IHttpActionResult List(string key)
        {
            if (string.IsNullOrEmpty(key) || key == "{}")
            {
                return List();
            }

            return GetHandler(
                key,
                () =>
                {
                    var entries = repository.Queryable<ReferenceData>().Where(x => x.Key == key).OrderBy(x => x.Value);

                    var list = new ReferenceDataList();
                    list.AddRange(entries.Select(entry => new EnergyTrading.Mdm.Contracts.ReferenceData { ReferenceKey = entry.Key, Value = entry.Value }));
                    Logger.DebugFormat("ReferenceData list created. Key: {0}", key);
                    return Ok(list);
                });
        }

        [HttpPost, Route("create/{key}")]
        public IHttpActionResult CreateList(string key, [FromBody] IList<EnergyTrading.Mdm.Contracts.ReferenceData> entries)
        {
            return PostHandler(
                key,
                () =>
                {
                    using (var scope = new TransactionScope())
                    {
                        var entities = repository.Queryable<ReferenceData>().Where(x => x.Key == key);
                        foreach (ReferenceData entity in entities)
                        {
                            repository.Delete(entity);
                        }

                        repository.Flush();

                        foreach (var entry in entries.Select(x => x.Value.Trim()).Distinct())
                        {
                            if (!repository.Queryable<ReferenceData>().Any(x => x.Key == key && x.Value.ToUpper() == entry.ToUpper()))
                            {
                                repository.Add(new ReferenceData { Key = key, Value = entry });
                                repository.Flush();
                            }
                        }

                        if (!repository.Queryable<ReferenceData>().Any(x => x.Key == key && x.Value == string.Empty))
                        {
                            repository.Add(new ReferenceData { Key = key, Value = string.Empty });
                        }

                        repository.Flush();
                        scope.Complete();
                    }

                    var location = String.Format("{0}/list/{1}",
                        Request.RequestUri.AbsolutePath.Substring(1),
                        key);

                    Logger.DebugFormat("ReferenceData list created. Key: {0}, Location: {1}", key, location);

                    return new StatusCodeResultWithLocation(Request, HttpStatusCode.Created, location);
                });
        }

        [HttpPost, Route("delete/{key}")]
        public void DeleteList(string key, [FromBody] IList<EnergyTrading.Mdm.Contracts.ReferenceData> entries)
        {
            PostHandler(
                key,
                () =>
                {
                    using (var scope = new TransactionScope())
                    {
                        foreach (var entry in entries.Select(x => x.Value).Distinct())
                        {
                            var entity = repository.Queryable<ReferenceData>().FirstOrDefault(x => x.Key == key && x.Value == entry);
                            if (entity != null)
                            {
                                repository.Delete(entity);
                            }
                        }

                        repository.Flush();
                        scope.Complete();
                    }

                    Logger.DebugFormat("ReferenceData list deleted. Key: {0}", key);

                    return Ok();
                });
        }

        private IHttpActionResult GetHandler(string key, Func<IHttpActionResult> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(
                    "Exception occurred while processing ReferenceData with key '{0}'. Exception message: {1}{2}Stack Trace: {3}.",
                    key,
                    ex.AllExceptionMessages(),
                    Environment.NewLine,
                    ex.StackTrace);

                return NotFound();

            }
            finally
            {
                var csp = serviceLocator.GetInstance<IDbContextProvider>();
                if (csp != null)
                {
                    csp.Close();
                }
            }
        }

        private IHttpActionResult PostHandler(string key, Func<IHttpActionResult> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(
                    "Exception occurred while processing ReferenceData with key '{0}'. Exception message: {1}{2}Stack Trace: {3}.",
                    key,
                    ex.AllExceptionMessages(),
                    Environment.NewLine,
                    ex.StackTrace);

                throw ex;
            }
            finally
            {
                var csp = serviceLocator.GetInstance<IDbContextProvider>();
                if (csp != null)
                {
                    csp.Close();
                }
            }
        }
    }
}