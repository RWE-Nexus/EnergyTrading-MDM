namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Legacy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Syndication;
    using System.ServiceModel.Web;
    using System.Transactions;

    using AutoMapper;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Logging;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Data.Search;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.Search;
    using EnergyTrading.ServiceModel.Channels;
    using EnergyTrading.Xml.Serialization;

    using global::MDM.ServiceHost.Wcf;

    using Microsoft.Practices.ServiceLocation;

    using ILegacyMdmEntity = RWEST.Nexus.MDM.Contracts.IMdmEntity;
    using IMdmEntity = EnergyTrading.Mdm.Contracts.IMdmEntity;
    using LegacyMapping = RWEST.Nexus.MDM.Contracts.Mapping;
    using LegacyMappingResponse = RWEST.Nexus.MDM.Contracts.MappingResponse;
    using LegacyNexusId = RWEST.Nexus.MDM.Contracts.NexusId;
    using Mapping = EnergyTrading.Mdm.Contracts.Mapping;
    using MappingResponse = EnergyTrading.Mdm.Contracts.MappingResponse;

    public abstract class LegacyEntityService<TLegacyContract, TContract, TEntity> : EntityService<TContract, TEntity>
        where TLegacyContract : class, ILegacyMdmEntity
        where TContract : class, IMdmEntity
        where TEntity : IEntity
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected IMappingEngine MappingEngine { get; set; }

        private readonly LegacyFeedFactory feedFactory;

        protected LegacyEntityService()
        {
            MappingEngine = ServiceLocator.Current.GetInstance<IMappingEngine>();

            feedFactory = new LegacyFeedFactory(MappingEngine);
        }

        public void CreateHandler(TLegacyContract contract)
        {
            try
            {
                base.CreateHandler(MappingEngine.Map<TLegacyContract, TContract>(contract));
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public void CreateMappingHandler(string id, LegacyMapping mapping)
        {
            try
            {
                base.CreateMappingHandler(id, MappingEngine.Map<LegacyMapping, Mapping>(mapping));
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public new TLegacyContract GetHandler(string id)
        {
            try
            {
                return MappingEngine.Map<TContract, TLegacyContract>(base.GetHandler(id));
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public new IList<TLegacyContract> GetEntityListHandler(string id)
        {
            try
            {
                return base.GetEntityListHandler(id).Select(x => MappingEngine.Map<TContract, TLegacyContract>(x)).ToList();
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public new IList<TLegacyContract> ListHandler()
        {
            try
            {
                return base.ListHandler().Select(x => MappingEngine.Map<TContract, TLegacyContract>(x)).ToList();
            }
            catch (WebFaultException<Fault> exc)
            {                
                throw MapAndThrow(exc);
            }
        }

        private WebFaultException<RWEST.Nexus.MDM.Contracts.Fault> MapAndThrow(WebFaultException<Fault> exc)
        {
            var legacyFault = MappingEngine.Map<EnergyTrading.Mdm.Contracts.Fault, RWEST.Nexus.MDM.Contracts.Fault>(exc.Detail);
            return new WebFaultException<RWEST.Nexus.MDM.Contracts.Fault>(legacyFault, exc.StatusCode);
        }

        public new TLegacyContract MapHandler()
        {
            try
            {
                return MappingEngine.Map<TContract, TLegacyContract>(base.MapHandler());
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public new LegacyMappingResponse CrossMapHandler()
        {
            try
            {
                return MappingEngine.Map<MappingResponse, LegacyMappingResponse>(base.CrossMapHandler());
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public new LegacyMappingResponse MappingHandler(string id, string mappingId)
        {
            try
            {
                return MappingEngine.Map<MappingResponse, LegacyMappingResponse>(base.MappingHandler(id, mappingId));
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public void UpdateMappingHandler(string id, string mappingId, LegacyMapping mapping)
        {
            try
            {
                base.UpdateMappingHandler(id, mappingId, MappingEngine.Map<LegacyMapping, Mapping>(mapping));
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }
        
        public override Message FeedHandler()
        {
            try
            {
                return this.WebHandler(
                    delegate
                    {
                        Logger.DebugFormat("Feed requested for {0}.", typeof(TContract).Name);

                        IList<TContract> response = this.GetList();

                        var feed = new SyndicationFeed();
                        feed.Id = string.Format("urn:uuid:{0}:{1}", this.BaseUrl, "list");
                        feed.Title = new TextSyndicationContent("All");
                        feed.Generator = "Nexus Mapping Service";
                        feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
                        //feed.LastUpdatedTime = response.Contract.Audit.LastChangeTimestamp;

                        feed.Items = response.Select(x => new SyndicationItem
                        {
                            Title = new TextSyndicationContent(this.BaseUrl),
                            Content = new XmlSyndicationContent(this.MdmContentType, new SyndicationElementExtension(MappingEngine.Map<TContract, TLegacyContract>(x)))
                        });

                        var formatter = new Atom10FeedFormatter(feed);

                        return Message.CreateMessage(MessageVersion.None, "Mapping", new FeedBodyWriter(formatter));
                    });
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }


        public override Message SearchHandler(Search search)
        {
            try
            {
                return this.WebHandler(
                    delegate
                    {
                        Logger.DebugFormat("Search for {0}: {1}", typeof(TContract).Name, search.DataContractSerialize());
                        const string FirstPage = "1";
                        var key = search.ToKey<TContract>();
                        var searchResults = this.Service.GetSearchResults(key, int.Parse(FirstPage));
                        if (searchResults == null)
                        {
                            this.Service.CreateSearch(search);
                            searchResults = this.Service.GetSearchResults(key, int.Parse(FirstPage));
                        }
                        if (searchResults == null)
                        {
                            this.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                            throw FaultFactory.SearchResultNotFoundException(key, FirstPage);
                        }
                        Logger.DebugFormat("Search for {0} Complete", typeof(TContract).Name);
                        return this.feedFactory.CreateFeed<TLegacyContract, TContract>(searchResults, this.EntityName, this.OutgoingResponse.BaseUri);
                    });
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public override Message DirectSearchHandler(IEntitySearchCommand<TEntity> searchCommand, Search search)
        {
            try
            {
                var result = this.Service.CreateDirectSearch(search, searchCommand);

                if (result.Contracts.Count == 0)
                {
                    return null;
                }

                return this.feedFactory.CreateFeed<TLegacyContract, TContract>(result, this.EntityName, this.OutgoingResponse.BaseUri);
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public override Message NonTemporalSearchHandler(Search search)
        {
            try
            {
                var result = this.Service.CreateNonTemporalSearch(search);

                if (result.Contracts.Count == 0)
                {
                    return null;
                }

                return this.feedFactory.CreateFeed<TLegacyContract, TContract>(result, this.EntityName, this.OutgoingResponse.BaseUri);
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        private SearchResultPage<TContract> CreateSearchAndGetPage(string key, int pageNumber)
        {
            this.Service.CreateSearch(key.ToSearch<TContract>());
            return this.Service.GetSearchResults(key, pageNumber);
        }

        public override Message SearchResultsHandler(string searchKey, string pageNumber)
        {
            try
            {
                Logger.DebugFormat("Search for {0}: SearchKey: {1}, PageNumber: {2}", typeof(TContract).Name, searchKey, pageNumber);

                var page = int.Parse(pageNumber);
                var searchResults = this.Service.GetSearchResults(searchKey, page) ?? this.CreateSearchAndGetPage(searchKey, page);
                if (string.IsNullOrEmpty(searchKey))
                {
                    this.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                    throw FaultFactory.SearchResultNotFoundException(searchKey, pageNumber);
                }
                Logger.DebugFormat("Search for {0}: SearchKey: {1}, PageNumber: {2} Complete", typeof(TContract).Name, searchKey, pageNumber);
                return this.feedFactory.CreateFeed<TLegacyContract, TContract>(searchResults, this.EntityName, this.OutgoingResponse.BaseUri);
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public override Message MappingsHandler(string id)
        {
            try
            {
                return this.WebHandler(
                    delegate
                    {
                        Logger.DebugFormat("Get mappings for {0}: {1}", typeof(TContract).Name, id);

                        var request = MessageFactory.GetRequest(this.QueryParameters);
                        request.EntityId = int.Parse(id);
                        request.Version = this.ReadVersion();

                        ContractResponse<TContract> response;
                        using (var scope = new TransactionScope(TransactionScopeOption.Required, ReadOptions()))
                        {
                            response = this.Service.Request(request);
                            scope.Complete();
                        }

                        if (!response.IsValid)
                        {
                            throw FaultFactory.Exception(this.EntityName, response, request);
                        }

                        var feed = new SyndicationFeed();
                        feed.Id = string.Format("urn:uuid:{0}:{1}", this.BaseUrl, id);
                        feed.Title = new TextSyndicationContent(string.Format("Mappings for {0} {1} ", this.EntityName, id));
                        feed.Generator = "Nexus Mapping Service";
                        feed.Authors.Add(new SyndicationPerson { Name = "Nexus Mapping Service" });
                        //feed.LastUpdatedTime = response.Contract.Audit.LastChangeTimestamp;

                        feed.Items = response.Contract.Identifiers.Select(mapping => new SyndicationItem
                        {
                            Title = new TextSyndicationContent("mapping"),
                            Content = new XmlSyndicationContent(this.MdmContentType, new SyndicationElementExtension(MappingEngine.Map<MdmId, LegacyNexusId>(mapping)))
                        });

                        var formatter = new Atom10FeedFormatter(feed);

                        return Message.CreateMessage(MessageVersion.None, "Mapping", new FeedBodyWriter(formatter));
                    });
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }

        public void UpdateHandler(string id, TLegacyContract contract)
        {
            try
            {
                base.UpdateHandler(id, MappingEngine.Map<TLegacyContract, TContract>(contract));
            }
            catch (WebFaultException<Fault> exc)
            {
                throw MapAndThrow(exc);
            }
        }
    }
}