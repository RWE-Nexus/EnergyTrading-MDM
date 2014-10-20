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
    /// The ReferenceData MDM resource is not a standard MDM entity or contract and as such does not adhere to the 
    /// same mechanisms as the other entities.  It therefore requires its own custom controller and routes as described
    /// here.
    /// NOTE: Some of the non-REST standard routes here are required for backwards compatibility with existing consumers.
    /// </summary>
    [RoutePrefix("referencedata")]
    public class ReferenceDataController : ApiController
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRepository repository;
        private readonly IServiceLocator serviceLocator;

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

        /// <summary>
        /// List the ReferenceData entries for the category key supplied.  
        /// If no key is supplied then ALL ReferenceData entities are returned.
        /// </summary>
        /// <param name="key">The ReferenceData category key</param>
        /// <returns>Response with approriate status code and the list of ReferenceData as content</returns>
        [HttpGet, Route("list/{key?}"), Route("{key?}")]
        public IHttpActionResult List(string key = null)
        {
            if (string.IsNullOrEmpty(key) || key == "{}")
            {
                return ListAll();
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

        /// <summary>
        /// Creates a new list of ReferenceData entries for the given category key
        /// </summary>
        /// <param name="key">The category key</param>
        /// <param name="entries">The list of new entries</param>
        /// <returns>Response with approriate status code and the query url for retrieving them</returns>
        [HttpPost, Route("create/{key}"), Route("{key}")]
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

        /// <summary>
        /// Removes the supplied entries for the category key given
        /// </summary>
        /// <param name="key">The category key</param>
        /// <param name="entries">The list of entries to remove</param>
        [HttpDelete, Route("{key}")]
        public void Delete(string key, [FromBody] IList<EnergyTrading.Mdm.Contracts.ReferenceData> entries)
        {
            DeleteList(key, entries);
        }

        /// <summary>
        /// Removes the supplied entries for the category key given
        /// </summary>
        /// <param name="key">The category key</param>
        /// <param name="entries">The list of entries to remove</param>
        [HttpPost, Route("delete/{key}")]
        public void PostDelete(string key, [FromBody] IList<EnergyTrading.Mdm.Contracts.ReferenceData> entries)
        {
            DeleteList(key, entries);
        }

        private IHttpActionResult ListAll()
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

        private void DeleteList(string key, IEnumerable<EnergyTrading.Mdm.Contracts.ReferenceData> entries)
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
                // NB Closes EF connection explcitly to avoid leaks in integration tests
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
                // NB Closes EF connection explcitly to avoid leaks in integration tests
                var csp = serviceLocator.GetInstance<IDbContextProvider>();
                if (csp != null)
                {
                    csp.Close();
                }
            }
        }
    }
}