namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus
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

    using EnergyTrading.Data;
    using EnergyTrading.Extensions;
    using EnergyTrading.Logging;
    using EnergyTrading.Web;

    using global::MDM.ServiceHost.Wcf;

    using OpenNexus.MDM.Contracts;using EnergyTrading.Mdm.Contracts;

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
                return this.GetHandler(
                    key, 
                    () =>
                    {
                        var entries = this.repository.Queryable<MDM.ReferenceData>().OrderBy(x => x.Key + "|" + x.Value);

                        var list = new ReferenceDataList();
                        list.AddRange(entries.Select(entry => new ReferenceData {ReferenceKey = entry.Key, Value = entry.Value }));

                        return list;
                    });
            }
            else
            {
                return this.GetHandler(
                    key, 
                    () =>
                    {
                        var entries = this.repository.Queryable<MDM.ReferenceData>().Where(x => x.Key == key).OrderBy(x => x.Value);

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
                        var entities = this.repository.Queryable<MDM.ReferenceData>().Where(x => x.Key == key);
                        foreach (MDM.ReferenceData entity in entities)
                        {
                            this.repository.Delete(entity);
                        }

                        this.repository.Flush();

                        foreach (var entry in entries.Select(x => x.Value.Trim()).Distinct())
                        {
                            if (!this.repository.Queryable<MDM.ReferenceData>().Any(x => x.Key == key && x.Value.ToUpper() == entry.ToUpper()))
                            {
                                this.repository.Add(new MDM.ReferenceData() { Key = key, Value = entry });
                                this.repository.Flush();
                            }
                        }

                        if (!this.repository.Queryable<MDM.ReferenceData>().Any(x => x.Key == key && x.Value == string.Empty))
                        {
                            this.repository.Add(new MDM.ReferenceData() { Key = key, Value = string.Empty });
                        }

                        this.repository.Flush();
                        scope.Complete();
                    }

                    this.contextWrapper.StatusCode = HttpStatusCode.Created;
                    this.contextWrapper.Location = string.Format("{0}/list/{1}", this.contextWrapper.InboundAbsoloutePath, key);

                    Logger.DebugFormat("ReferenceData list created. Key: {0}, Location: {1}", key, this.contextWrapper.Location);
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
                            var entity = this.repository.Queryable<MDM.ReferenceData>().FirstOrDefault(x => x.Key == key && x.Value == entry);
                            if (entity != null)
                            {
                                this.repository.Delete(entity);
                            }
                        }

                        this.repository.Flush();
                        scope.Complete();
                    }

                    this.contextWrapper.StatusCode = HttpStatusCode.OK;

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