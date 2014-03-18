namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class LocationDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Type { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId Parent { get; set; }
    }
}