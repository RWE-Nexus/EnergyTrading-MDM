namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus
{
    using System.Collections.Generic;
    using System.Net;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Web;

    using EnergyTrading.MDM.Data.Search;

    using global::MDM.ServiceHost.Wcf;

    using EnergyTrading.Contracts.Search;
    using OpenNexus.MDM.Contracts;using EnergyTrading.Mdm.Contracts;

    [ServiceContract]
    // [XmlSerializerFormat]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BrokerRateService : EntityService<OpenNexus.MDM.Contracts.BrokerRate, MDM.BrokerRate>
    {
        /// <summary>
        /// Gets the BaseUrl property.
        /// <para>
        /// The root part of the url for this service.
        /// </para>
        /// </summary>
        protected override string BaseUrl
        {
            get { return "brokerrate"; }
        }

        protected override string EntityName
        {
            get { return "BrokerRate"; }
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public void Create(BrokerRate contract)
        {
            this.CreateHandler(contract);
        }

        [WebInvoke(UriTemplate = "{id}/mapping", Method = "POST")]
        public void CreateMapping(string id, Mapping mapping)
        {
            this.CreateMappingHandler(id, mapping);
        }

        //the id for the entity is not required but is used to make the url more descriptive to the consumer
        [WebInvoke(UriTemplate = "{entityId}/mapping/{mappingId}", Method = "DELETE")]
        public void DeleteMapping(string entityId, string mappingId)
        {
            this.DeleteMappingHandler(entityId, mappingId);
        }

        [WebGet(UriTemplate = "list")]
        public BrokerRateList List()
        {
            var list = new BrokerRateList();
            list.AddRange(this.ListHandler());

            return list;
        }

        [DataContractFormat]
        [WebGet(UriTemplate = "feed")]
        public Message Feed()
        {
            return this.FeedHandler();
        }

        [WebInvoke(UriTemplate = "search", Method = "POST")]
        public Message Search(EnergyTrading.Contracts.Search.Search search)
        {
            if (search.SearchOptions.IsMappingSearch)
            {
                return this.SearchHandler(search);
            }

            var entitySearchCommand = new BrokerRateSearchCommand();
            var result = this.DirectSearchHandler(entitySearchCommand, search);

            if (result == null)
            {
                this.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                return Message.CreateMessage(MessageVersion.None, "NotFound");
            }

            return result;
        }

        [WebGet(UriTemplate = "search?key={searchKey}&page={pageNumber}")]
        public Message SearchResults(string searchKey, string pageNumber)
        {
            return this.SearchResultsHandler(searchKey, pageNumber);
        }

        [DataContractFormat]
        [WebGet(UriTemplate = "{id}/mappings")]
        // [AspNetCacheProfile("MdmMappingCache")]
        public Message Mappings(string id)
        {
            return this.MappingsHandler(id);
        }
		
        [DataContractFormat]
        [WebGet(UriTemplate = "{id}/list")]
        //[AspNetCacheProfile("MdmEntityCache")]
        public IList<BrokerRate> GetEntityList(string id)
        {
            var list = new List<BrokerRate>();
            list.AddRange(this.GetEntityListHandler(id));

            return list;
        }

        [WebGet(UriTemplate = "{id}")]
        //[AspNetCacheProfile("MdmEntityCache")]
        public BrokerRate Get(string id)
        {
            return this.GetHandler(id);
        }

        [WebGet(UriTemplate = "map")]
        // [AspNetCacheProfile("MdmMappingCache")]
        public BrokerRate Map()
        {
            return this.MapHandler();
        }

        [WebGet(UriTemplate = "crossmap")]
        // [AspNetCacheProfile("MdmMappingCache")]
        public MappingResponse CrossMap()
        {
            return this.CrossMapHandler();
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            this.DeleteHandler(id);
        }

        [WebInvoke(UriTemplate = "{id}/mapping/{mappingId}", Method = "GET")]
        public MappingResponse Mapping(string id, string mappingId)
        {
            return this.MappingHandler(id, mappingId);
        }

        [WebInvoke(UriTemplate = "{id}/mapping/{mappingId}", Method = "POST")]
        public void UpdateMapping(string id, string mappingId, Mapping mapping)
        {
            this.UpdateMappingHandler(id, mappingId, mapping);
        }

        [WebInvoke(UriTemplate = "{id}", Method = "POST")]
        public void Update(string id, BrokerRate contract)
        {
            this.UpdateHandler(id, contract);
        }
    }
}