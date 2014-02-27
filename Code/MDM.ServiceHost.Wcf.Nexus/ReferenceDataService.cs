namespace EnergyTrading.MDM.MappingService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using System.Transactions;

    using EnergyTrading.Logging;

    using EnergyTrading.Data;
    using EnergyTrading.Extensions;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Web;

    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReferenceDataService
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRepository repository;
        private readonly IWebOperationContextWrapper contextWrapper;

        public ReferenceDataService()
        {
            this.repository = Global.ServiceLocator.GetInstance<IRepository>();
            this.contextWrapper = Global.ServiceLocator.GetInstance<IWebOperationContextWrapper>();
        }

        [WebGet(UriTemplate = "list/{key}")]
        public ReferenceDataList List(string key)
        {
            if (key == "{}")
            {
                return GetHandler(
                    key, 
                    () =>
                    {
                        var entries = repository.Queryable<MDM.ReferenceData>().OrderBy(x => x.Key + "|" + x.Value);

                        var list = new ReferenceDataList();
                        list.AddRange(entries.Select(entry => new ReferenceData {ReferenceKey = entry.Key, Value = entry.Value }));

                        return list;
                    });
            }
            else
            {
                return GetHandler(
                    key, 
                    () =>
                    {
                        var entries = repository.Queryable<MDM.ReferenceData>().Where(x => x.Key == key).OrderBy(x => x.Value);

                        var list = new ReferenceDataList();
                        list.AddRange(entries.Select(entry => new ReferenceData {ReferenceKey = entry.Key,  Value = entry.Value }));

                        return list;
                    });
            }
        }

        [WebInvoke(UriTemplate = "create/{key}", Method = "POST")]
        public void CreateList(string key, IList<ReferenceData> entries)
        {
            this.PostHandler(
                key, 
                () =>
                {
                    using (var scope = new TransactionScope())
                    {
                        var entities = repository.Queryable<MDM.ReferenceData>().Where(x => x.Key == key);
                        foreach (MDM.ReferenceData entity in entities)
                        {
                            repository.Delete(entity);
                        }

                        repository.Flush();

                        foreach (var entry in entries.Select(x => x.Value.Trim()).Distinct())
                        {
                            if (!repository.Queryable<MDM.ReferenceData>().Any(x => x.Key == key && x.Value.ToUpper() == entry.ToUpper()))
                            {
                                repository.Add(new MDM.ReferenceData() { Key = key, Value = entry });
                                repository.Flush();
                            }
                        }

                        if (!repository.Queryable<MDM.ReferenceData>().Any(x => x.Key == key && x.Value == string.Empty))
                        {
                            repository.Add(new MDM.ReferenceData() { Key = key, Value = string.Empty });
                        }

                        repository.Flush();
                        scope.Complete();
                    }

                    contextWrapper.StatusCode = HttpStatusCode.Created;
                    contextWrapper.Location = string.Format("{0}/list/{1}", contextWrapper.InboundAbsoloutePath, key);

                    Logger.DebugFormat("ReferenceData list created. Key: {0}, Location: {1}", key, contextWrapper.Location);
                });
        }

        [WebInvoke(UriTemplate = "delete/{key}", Method = "POST")]
        public void DeleteList(string key, IList<ReferenceData> entries)
        {
            this.PostHandler(
                key, 
                () =>
                {
                    using (var scope = new TransactionScope())
                    {
                        foreach (var entry in entries.Select(x => x.Value).Distinct())
                        {
                            var entity = repository.Queryable<MDM.ReferenceData>().FirstOrDefault(x => x.Key == key && x.Value == entry);
                            if (entity != null)
                            {
                                repository.Delete(entity);
                            }
                        }

                        repository.Flush();
                        scope.Complete();
                    }

                    contextWrapper.StatusCode = HttpStatusCode.OK;

                    Logger.DebugFormat("ReferenceData list deleted. Key: {0}", key);
                });
        }

        private ReferenceDataList GetHandler(string key, Func<ReferenceDataList> func)
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

                throw FaultFactory.Exception(ex);
            }
        }

        private IList<ReferenceData> GetHandlerEx(Func<IList<ReferenceData>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(
                    "Exception occurred while processing ReferenceData. Exception message: {0}{1}Stack Trace: {2}.",
                    ex.AllExceptionMessages(),
                    Environment.NewLine,
                    ex.StackTrace);

                throw FaultFactory.Exception(ex);
            }
        }

        private void PostHandler(string key, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(
                    "Exception occurred while processing ReferenceData with key '{0}'. Exception message: {1}{2}Stack Trace: {3}.",
                    key,
                    ex.AllExceptionMessages(),
                    Environment.NewLine,
                    ex.StackTrace);

                throw FaultFactory.Exception(ex);
            }
        }
    }
}