namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductScotaDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Product { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string ScotaContract { get; set; }


        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId ScotaDeliveryPoint { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public EntityId ScotaOrigin { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public string ScotaRss { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public string ScotaVersion { get; set; }      
    }
}