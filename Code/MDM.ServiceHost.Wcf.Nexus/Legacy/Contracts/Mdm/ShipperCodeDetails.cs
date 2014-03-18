namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ShipperCodeDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public EntityId Party { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Location { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string Code { get; set; }
    }
}
