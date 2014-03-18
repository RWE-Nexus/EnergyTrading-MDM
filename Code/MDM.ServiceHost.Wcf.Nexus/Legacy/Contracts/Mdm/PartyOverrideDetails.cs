namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PartyOverrideDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public EntityId Broker { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId CommodityInstrumentType { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string MappingValue { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId Party { get; set; }
    }
}