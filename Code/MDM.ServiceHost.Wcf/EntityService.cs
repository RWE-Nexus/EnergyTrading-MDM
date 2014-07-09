namespace EnergyTrading.Mdm.ServiceHost.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Syndication;
    using System.ServiceModel.Web;
    using System.Transactions;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Extensions;
    using EnergyTrading.Logging;
    using EnergyTrading.Mdm;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Data.Search;
    using EnergyTrading.Mdm.Extensions;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Mdm.ServiceHost.Wcf;
    using EnergyTrading.Mdm.ServiceHost.Wcf.Feeds;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Search;
    using EnergyTrading.ServiceModel.Channels;
    using EnergyTrading.Validation;
    using EnergyTrading.Web;
    using EnergyTrading.Xml.Serialization;

    using Microsoft.Practices.ServiceLocation;

    public abstract class EntityService<TContract, TEntity>
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IMdmService<TContract, TEntity> service;
        private readonly IWebOperationContextWrapper contextWrapper;

        private IFeedFactory feedFactory;

        protected EntityService()
        {
            MdmContentType = ConfigurationManager.AppSettings["Mdm.ContentType"];

            service = ServiceLocator.Current.GetInstance<IMdmService<TContract, TEntity>>();
            contextWrapper = ServiceLocator.Current.GetInstance<IWebOperationContextWrapper>();
            feedFactory = ServiceLocator.Current.GetInstance<IFeedFactory>();
        }

        protected string MdmContentType { get; set; }

        /// <summary>
        /// Gets the 
        /// BaseUrl property.
        /// <para>
        /// The root part of the url for this service.
        /// </para>
        /// </summary>
        protected abstract string BaseUrl { get; }

        protected abstract string EntityName { get; }

        protected IMdmService<TContract, TEntity> Service
        {
            get { return service; }
        }

        protected NameValueCollection QueryParameters
        {
            get { return contextWrapper.QueryParameters; }
        }

        protected IWebOperationContextWrapper OutgoingResponse
        {
            get { return contextWrapper; }
        }

        private static IsolationLevel ReadIsolation
        {
            get { return ConfigIsolation("Mdm.ReadIsolation"); }
        }

        private static IsolationLevel WriteIsolation
        {
            get { return ConfigIsolation("Mdm.WriteIsolation"); }
        }

        public virtual void CreateHandler(TContract contract)
        {
            WebHandler(() =>
            {
                Logger.DebugFormat(
                    "Creating {0}: {1}",
                    typeof(TContract).Name,
                    contract.DataContractSerialize());

                TEntity entity;

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    entity = Service.Create(contract);
                    scope.Complete();
                }

                OutgoingResponse.Location = string.Format(
                    "{0}/{1}?" + QueryConstants.ValidAt + "={2}",
                    OutgoingResponse.InboundAbsoloutePath,
                    entity.Id,
                    entity.Validity.Start.ToString(QueryConstants.DateFormatString));
                OutgoingResponse.StatusCode = HttpStatusCode.Created;

                Logger.DebugFormat("{0} created. EntityId: {1}, Location: {2}", typeof(TContract).Name, entity.Id, this.OutgoingResponse.Location);
            });
        }

        public virtual void CreateMappingHandler(string id, Mapping mapping)
        {
            WebHandler(() =>
            {
                Logger.DebugFormat(
                    "Creating mapping for {0}-{1}: {2}",
                    typeof(TContract).Name,
                    id,
                    mapping.DataContractSerialize());

                var request = new CreateMappingRequest
                {
                    EntityId = int.Parse(id),
                    Mapping = mapping,
                };
                IEntityMapping entityMapping;

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    entityMapping = Service.CreateMapping(request);
                    scope.Complete();
                }

                OutgoingResponse.Location = string.Format("{0}/{1}/mapping/{2}", OutgoingResponse.InboundAbsoloutePath, id, entityMapping.Id);
                OutgoingResponse.StatusCode = HttpStatusCode.Created;

                Logger.DebugFormat("Mappings created for {0}-{1}. EntityMappingId: {2}, Location: {3}", typeof(TContract).Name, id, entityMapping.Id, OutgoingResponse.Location);
            });
        }

        public virtual void DeleteMappingHandler(string entityId, string mappingId)
        {
            WebHandler(() =>
            {
                Logger.DebugFormat("Deleting mapping for {0}-{1}: MappingId: {2}", typeof(TContract).Name, entityId, mappingId);

                OutgoingResponse.ContentType = this.MdmContentType;

                var request = new DeleteMappingRequest
                {
                    MappingId = int.Parse(mappingId)
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    this.Service.DeleteMapping(request);
                    scope.Complete();
                }

                OutgoingResponse.StatusCode = HttpStatusCode.OK;

                Logger.DebugFormat("Mapping deleted for {0}-{1}: MappingId: {2}", typeof(TContract).Name, entityId, mappingId);
            });
        }

        public virtual MappingResponse CrossMapHandler()
        {
            return this.ContractHandler(() =>
            {
                Logger.DebugFormat("CrossMap requested for {0}.", typeof(TContract).Name);
                OutgoingResponse.ContentType = MdmContentType;

                var request = MessageFactory.CrossMappingRequest(QueryParameters);
                request.Version = ReadVersion();

                ContractResponse<MappingResponse> response;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = Service.CrossMap(request);
                    scope.Complete();
                }

                if (response.IsValid)
                {
                    return response;
                }

                throw FaultFactory.Exception(EntityName, response, request);
            });
        }

        public virtual void DeleteHandler(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Message FeedHandler()
        {
            return WebHandler(() =>
            {
                Logger.DebugFormat("Feed requested for {0}.", typeof(TContract).Name);

                var response = GetList();

                var feed = new SyndicationFeed();
                feed.Id = string.Format("urn:uuid:{0}:{1}", this.BaseUrl, "list");
                feed.Title = new TextSyndicationContent("All");
                feed.Generator = "Nexus Mapping Service";
                feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
                //feed.LastUpdatedTime = response.Contract.Audit.LastChangeTimestamp;

                feed.Items = response.Select(x => new SyndicationItem
                {
                    Title = new TextSyndicationContent(BaseUrl),
                    Content = new XmlSyndicationContent(MdmContentType, new SyndicationElementExtension(x))
                });

                var formatter = new Atom10FeedFormatter(feed);

                return Message.CreateMessage(MessageVersion.None, "Mapping", new FeedBodyWriter(formatter));
            });
        }

        public virtual TContract GetHandler(string id)
        {
            return this.ContractHandler(() =>
            {
                Logger.DebugFormat("Get {0} entity: EntityId: {1}.", typeof(TContract).Name, id);

                OutgoingResponse.ContentType = MdmContentType;
                var request = MessageFactory.GetRequest(QueryParameters);
                request.EntityId = int.Parse(id);
                request.Version = ReadVersion();

                ContractResponse<TContract> response;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = Service.Request(request);
                    scope.Complete();
                }

                if (response.IsValid)
                {
                    return response;
                }

                throw FaultFactory.Exception(EntityName, response, request);
            });
        }

        public virtual IList<TContract> GetEntityListHandler(string id)
        {
            return WebHandler(() =>
            {
                Logger.DebugFormat("Get entity list for {0} entity: EntityId: {1}.", typeof(TContract).Name, id);

                OutgoingResponse.ContentType = MdmContentType;
                    
                var request = MessageFactory.GetRequest(QueryParameters);
                request.EntityId = int.Parse(id);

                IEnumerable<TContract> result;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    result = Service.EntityList(request);
                    scope.Complete();
                }

                var resultList = result.ToList();
                if (resultList.Any())
                {
                    return resultList;
                }

                var error = new ContractError { Reason = ErrorReason.Identifier, Type = ErrorType.NotFound };
                throw FaultFactory.NotFoundException<TContract>(EntityName, error, request);
            });
        }

        public virtual IList<TContract> ListHandler()
        {
            return WebHandler(() =>
            {
                Logger.DebugFormat("Get {0} list.", typeof(TContract).Name);

                OutgoingResponse.ContentType = MdmContentType;

                return GetList();
            });
        }

        public virtual TContract MapHandler()
        {
            return this.ContractHandler(() =>
            {
                Logger.DebugFormat("Map requested for {0}.", typeof(TContract).Name);

                OutgoingResponse.ContentType = MdmContentType;

                var request = MessageFactory.MappingRequest(QueryParameters);
                request.Version = ReadVersion();

                ContractResponse<TContract> response;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = Service.Map(request);
                    scope.Complete();
                }

                if (response.IsValid)
                {
                    return response;
                }

                throw FaultFactory.Exception(EntityName, response, request);
            });
        }

        public virtual MappingResponse MappingHandler(string id, string mappingId)
        {
            return this.ContractHandler(() =>
            {
                Logger.DebugFormat("Get mapping for {0}-{1}: MappingId: {2}.", typeof(TContract).Name, id, mappingId);

                OutgoingResponse.ContentType = MdmContentType;

                var request = new GetMappingRequest
                {
                    EntityId = int.Parse(id),
                    MappingId = int.Parse(mappingId),
                };

                ContractResponse<MappingResponse> response;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = Service.RequestMapping(request);
                    scope.Complete();
                }

                if (response.IsValid)
                {
                    return response;
                }

                throw FaultFactory.Exception(EntityName, response, request);
            });
        }

        public virtual Message SearchHandler(Search search)
        {
            return this.WebHandler(() =>
            {
                Logger.DebugFormat("Search for {0}: {1}", typeof(TContract).Name, search.DataContractSerialize());
                const string FirstPage = "1";
                var key = search.ToKey<TContract>();
                var searchResults = Service.GetSearchResults(key, int.Parse(FirstPage));
                if (searchResults == null)
                {
                    Service.CreateSearch(search);
                    searchResults = Service.GetSearchResults(key, int.Parse(FirstPage));
                }
                if (searchResults == null)
                {
                    OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                    throw FaultFactory.SearchResultNotFoundException(key, FirstPage);
                }
                Logger.DebugFormat("Search for {0} Complete", typeof(TContract).Name);
                return feedFactory.CreateFeed<TContract>(searchResults, this.EntityName, this.OutgoingResponse.BaseUri);
            });
        }

        public virtual Message DirectSearchHandler(IEntitySearchCommand<TEntity> searchCommand, Search search)
        {
            var result = Service.CreateDirectSearch(search, searchCommand);

            if (result.Contracts.Count == 0)
            {
                return null;
            }

            return feedFactory.CreateFeed(result, this.EntityName, this.OutgoingResponse.BaseUri);
        }

        public virtual Message NonTemporalSearchHandler(Search search)
        {
            var result = Service.CreateNonTemporalSearch(search);

            if (result.Contracts.Count == 0)
            {
                return null;
            }

            return feedFactory.CreateFeed(result, this.EntityName, this.OutgoingResponse.BaseUri);
        }

        private SearchResultPage<TContract> CreateSearchAndGetPage(string key, int pageNumber)
        {
            Service.CreateSearch(key.ToSearch<TContract>());
            return Service.GetSearchResults(key, pageNumber);
        }

        public virtual Message SearchResultsHandler(string searchKey, string pageNumber)
        {
            Logger.DebugFormat("Search for {0}: SearchKey: {1}, PageNumber: {2}", typeof(TContract).Name, searchKey, pageNumber);

            var page = int.Parse(pageNumber);
            var searchResults = Service.GetSearchResults(searchKey, page) ?? this.CreateSearchAndGetPage(searchKey, page);
            if (string.IsNullOrEmpty(searchKey))
            {
                OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                throw FaultFactory.SearchResultNotFoundException(searchKey, pageNumber);
            }
            Logger.DebugFormat("Search for {0}: SearchKey: {1}, PageNumber: {2} Complete", typeof(TContract).Name, searchKey, pageNumber);
            return feedFactory.CreateFeed<TContract>(searchResults, this.EntityName, this.OutgoingResponse.BaseUri);
        }

        public virtual Message MappingsHandler(string id)
        {
            return WebHandler(() =>
            {
                Logger.DebugFormat("Get mappings for {0}: {1}", typeof(TContract).Name, id);

                var request = MessageFactory.GetRequest(QueryParameters);
                request.EntityId = int.Parse(id);
                request.Version = ReadVersion();

                ContractResponse<TContract> response;
                using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                {
                    response = Service.Request(request);
                    scope.Complete();
                }

                if (!response.IsValid)
                {
                    throw FaultFactory.Exception(this.EntityName, response, request);
                }

                var feed = new SyndicationFeed
                {
                    Id = string.Format("urn:uuid:{0}:{1}", BaseUrl, id),
                    Title = new TextSyndicationContent(string.Format("Mappings for {0} {1} ", EntityName, id)),
                    Generator = "Nexus Mapping Service"
                };
                feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
                //feed.LastUpdatedTime = response.Contract.Audit.LastChangeTimestamp;

                feed.Items = response.Contract.Identifiers.Select(mapping => new SyndicationItem
                {
                    Title = new TextSyndicationContent("mapping"),
                    Content = new XmlSyndicationContent(MdmContentType, new SyndicationElementExtension(mapping))
                });

                var formatter = new Atom10FeedFormatter(feed);

                return Message.CreateMessage(MessageVersion.None, "Mapping", new FeedBodyWriter(formatter));
            });
        }

        public virtual void UpdateHandler(string id, TContract contract)
        {
            WebHandler(() =>
            {
                Logger.DebugFormat("Updating {0}-{1}: {2}", typeof(TContract).Name, id, contract.DataContractSerialize());

                var entityId = int.Parse(id);
                ContractResponse<TContract> returnedContract;

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    returnedContract = this.Service.Update(entityId, this.WriteVersion(), contract);
                    scope.Complete();
                }

                if (returnedContract.Contract != null)
                {
                    this.OutgoingResponse.Location = string.Format("{0}/{1}", this.OutgoingResponse.InboundAbsoloutePath, id);
                    this.OutgoingResponse.StatusCode = HttpStatusCode.NoContent;

                    Logger.DebugFormat("{0} updated. EntityId: {1}, Location: {2}", typeof(TContract).Name, id, this.OutgoingResponse.Location);

                    return;
                }

                this.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                Logger.DebugFormat("{0} not found for update. EntityId: {1}", typeof(TContract).Name, id);
            });
        }

        public virtual void UpdateMappingHandler(string id, string mappingId, Mapping mapping)
        {
            WebHandler(() =>
            {
                Logger.DebugFormat("Updating mapping for {0}-{1}: MappingId: {2}, Xml: {3}", typeof(TContract).Name, id, mappingId, mapping.DataContractSerialize());

                IEntityMapping returnedMapping = null;

                var request = new AmendMappingRequest
                {
                    EntityId = int.Parse(id),
                    MappingId = int.Parse(mappingId),
                    Mapping = mapping,
                    Version = this.WriteVersion()
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, WriteOptions()))
                {
                    returnedMapping = this.Service.UpdateMapping(request);
                    scope.Complete();
                }

                if (returnedMapping != null)
                {
                    this.OutgoingResponse.Location = string.Format("{0}/{1}/mapping/{2}", this.OutgoingResponse.InboundAbsoloutePath, id, mappingId);
                    this.OutgoingResponse.StatusCode = HttpStatusCode.NoContent;

                    Logger.DebugFormat("Mappings updated for {0}-{1}. MappingId: {2}, Location: {3}", typeof(TContract).Name, id, mappingId, this.OutgoingResponse.Location);
                    return;
                }

                this.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                Logger.DebugFormat("{0} mapping not found for update. EntityId: {1}, MappingId: {2}", typeof(TContract).Name, id, mappingId);
            });
        }

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

        protected ulong ReadVersion()
        {
            return Version("If-None-Match");
        }

        protected ulong WriteVersion()
        {
            return Version("If-Match");
        }

        protected ulong Version(string header)
        {
            var version = 0UL;
            var etag = contextWrapper.Headers[header];
            if (!string.IsNullOrEmpty(etag))
            {
                ulong.TryParse(etag.Trim('"'), out version);
            }

            return version;
        }

        /// <summary>
        /// Process an action, converting into a contract with ETag and content type support.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected T ContractHandler<T>(Func<ContractResponse<T>> action)
        {
            return this.WebHandler(
                delegate
                {
                    // Responsible for raising errors if it is invalid as it has the request message
                    var response = action.Invoke();

                    // Check if we have emitted this one before
                    this.contextWrapper.CheckConditionalRetrieve(response.Version.ToString());
                    this.OutgoingResponse.SetETag(response.Version.ToString());
                    this.OutgoingResponse.ContentType = this.MdmContentType;

                    return response.Contract;
                });
        }

        /// <summary>
        /// Process an action, handling exceptions into standard WCF responses.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected T WebHandler<T>(Func<T> action)
        {
            try
            {
                return action.Invoke();
            }
            catch (WebFaultException)
            {
                // Already dealt with
                throw;
            }
            catch (WebFaultException<Fault>)
            {
                // Already dealt with
                throw;
            }
            catch (VersionConflictException ex)
            {
                Logger.InfoFormat(LogMessages.EntityVersionConflictMessage, typeof(TContract).Name);
                throw FaultFactory.VersionConflictException(ex);
            }
            catch (ValidationException ex)
            {
                Logger.InfoFormat(LogMessages.EntityValidationMessage, typeof(TContract).Name, ex.Message);
                throw FaultFactory.ValidationException(ex);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(
                    "Exception occurred for {0} entity. Exception message: {1}{2}Stack Trace: {3}.",
                    typeof(TContract).Name,
                    ex.AllExceptionMessages(),
                    Environment.NewLine,
                    ex.StackTrace);

                throw FaultFactory.Exception(ex);
            }
        }

        protected void WebHandler(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (WebFaultException)
            {
                // Already dealt with
                throw;
            }
            catch (WebFaultException<Fault>)
            {
                // Already dealt with
                throw;
            }
            catch (VersionConflictException ex)
            {
                Logger.InfoFormat(LogMessages.EntityVersionConflictMessage, typeof(TContract).Name);
                throw FaultFactory.VersionConflictException(ex);
            }
            catch (ValidationException ex)
            {
                Logger.InfoFormat(LogMessages.EntityValidationMessage, typeof(TContract).Name, ex.Message);
                throw FaultFactory.ValidationException(ex);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(
                    "Exception occurred for {0} entity. Exception message: {1}{2}Stack Trace: {3}.",
                    typeof(TContract).Name,
                    ex.AllExceptionMessages(),
                    Environment.NewLine,
                    ex.StackTrace);

                throw FaultFactory.Exception(ex);
            }
        }

        protected IList<TContract> GetList()
        {
            List<TContract> list;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
            {
                // TODO: Constrain the identifiers we retrieve or have an enum to allow this e.g. Nexus, Originating, Default, All
                list = new List<TContract>(Service.List());
                scope.Complete();
            }

            return list;
        }

        private static IsolationLevel ConfigIsolation(string name, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var value = ConfigurationManager.AppSettings[name];
            IsolationLevel iso;
            return Enum.TryParse(value, out iso) ? iso : isolationLevel;
        }
    }
}