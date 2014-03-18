namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class CounterpartyDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string Phone { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string Fax { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public string ShortName { get; set; }
    }
}
