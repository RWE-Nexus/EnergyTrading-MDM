namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Web;

    using global::MDM.ServiceHost.Wcf;

    using RWEST.Nexus.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;

    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PartyRoleAccountabilityService : EntityService<RWEST.Nexus.MDM.Contracts.PartyRoleAccountability, MDM.PartyRoleAccountability>
    {
        /// <summary>
        /// Gets the BaseUrl property.
        /// <para>
        /// The root part of the url for this service.
        /// </para>
        /// </summary>
        protected override string BaseUrl
        {
            get { return "partyroleaccountability"; }
        }

        protected override string EntityName
        {
            get { return "PartyRoleAccountability"; }
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public void Create(PartyRoleAccountability contract)
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
        public PartyRoleAccountabilityList List()
        {
            var list = new PartyRoleAccountabilityList();
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
        public Message Search(RWEST.Nexus.Contracts.Search.Search search)
        {
            return this.SearchHandler(search.FromNexus());
        }

        [WebGet(UriTemplate = "search?key={searchKey}&page={pageNumber}")]
        public Message SearchResults(string searchKey, string pageNumber)
        {
            return this.SearchResultsHandler(searchKey, pageNumber);
        }

        [DataContractFormat]
        [WebGet(UriTemplate = "{id}/mappings")]
        public Message Mappings(string id)
        {
            return this.MappingsHandler(id);
        }

        [DataContractFormat]
        [WebGet(UriTemplate = "{id}/list")]
        public IList<PartyRoleAccountability> GetEntityList(string id)
        {
            var list = new List<PartyRoleAccountability>();
            list.AddRange(this.GetEntityListHandler(id));

            return list;
        }

        [WebGet(UriTemplate = "{id}")]
        public PartyRoleAccountability Get(string id)
        {
            return this.GetHandler(id);
        }

        [WebGet(UriTemplate = "map")]
        public PartyRoleAccountability Map()
        {
            return this.MapHandler();
        }

        [WebGet(UriTemplate = "crossmap")]
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
        public void Update(string id, PartyRoleAccountability contract)
        {
            this.UpdateHandler(id, contract);
        }
    }
}