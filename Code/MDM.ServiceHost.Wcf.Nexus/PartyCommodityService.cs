namespace EnergyTrading.MDM.MappingService
{
    using System.Collections.Generic;
    using System.Net;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Web;

    using RWEST.Nexus.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;

    [ServiceContract]
    // [XmlSerializerFormat]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PartyCommodityService
    {
        [WebInvoke(UriTemplate = "", Method = "POST")]
        public void Create(PartyCommodity contract)
        {
            this.InternalServerError();
        }

        [WebInvoke(UriTemplate = "{id}/mapping", Method = "POST")]
        public void CreateMapping(string id, Mapping mapping)
        {
            this.InternalServerError();
        }

        //the id for the entity is not required but is used to make the url more descriptive to the consumer
        [WebInvoke(UriTemplate = "{entityId}/mapping/{mappingId}", Method = "DELETE")]
        public void DeleteMapping(string entityId, string mappingId)
        {
            this.InternalServerError();
        }

        [WebGet(UriTemplate = "list")]
        public PartyCommodityList List()
        {
            this.NotFound();
            return null;
        }

        [DataContractFormat]
        [WebGet(UriTemplate = "feed")]
        public Message Feed()
        {
            this.NotFound();
            return null;
        }

        [WebInvoke(UriTemplate = "search", Method = "POST")]
        public void Search(Search search)
        {
            this.NotFound();
        }

        [WebGet(UriTemplate = "search?key={searchKey}&page={pageNumber}")]
        public Message SearchResults(string searchKey, string pageNumber)
        {
            this.NotFound();
            return null;
        }

        [DataContractFormat]
        [WebGet(UriTemplate = "{id}/mappings")]
        // [AspNetCacheProfile("MdmMappingCache")]
        public Message Mappings(string id)
        {
            this.NotFound();
            return null;
        }

        [DataContractFormat]
        [WebGet(UriTemplate = "{id}/list")]
        //[AspNetCacheProfile("MdmEntityCache")]
        public IList<Unit> GetEntityList(string id)
        {
            this.NotFound();
            return null;
        }

        [WebGet(UriTemplate = "{id}")]
        //[AspNetCacheProfile("MdmEntityCache")]
        public PartyCommodity Get(string id)
        {
            this.NotFound();
            return null;
        }

        [WebGet(UriTemplate = "map")]
        // [AspNetCacheProfile("MdmMappingCache")]
        public PartyCommodity Map()
        {
            this.NotFound();
            return null;
        }

        [WebGet(UriTemplate = "crossmap")]
        // [AspNetCacheProfile("MdmMappingCache")]
        public MappingResponse CrossMap()
        {
            this.NotFound();
            return null;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            this.InternalServerError();
        }

        [WebInvoke(UriTemplate = "{id}/mapping/{mappingId}", Method = "GET")]
        public MappingResponse Mapping(string id, string mappingId)
        {
            this.NotFound();
            return null;
        }

        [WebInvoke(UriTemplate = "{id}/mapping/{mappingId}", Method = "POST")]
        public void UpdateMapping(string id, string mappingId, Mapping mapping)
        {
            this.InternalServerError();
        }

        [WebInvoke(UriTemplate = "{id}", Method = "POST")]
        public void Update(string id, PartyCommodity contract)
        {
            this.InternalServerError();
        }

        private void InternalServerError()
        {
            var message = "The PartyCommodity entity is deprecated";
            var fault = new Fault { Message = message, Reason = message };

            throw new WebFaultException<Fault>(fault, HttpStatusCode.InternalServerError);
        }

        private void NotFound()
        {
            var message = "The PartyCommodity entity is deprecated";
            var fault = new Fault { Message = message, Reason = message };

            throw new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }
    }
}