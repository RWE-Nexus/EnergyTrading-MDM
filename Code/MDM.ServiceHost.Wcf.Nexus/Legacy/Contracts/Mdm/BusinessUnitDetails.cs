namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class BusinessUnitDetails
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

        [DataMember(Order = 5)]
        [XmlElement]
        public EntityId TaxLocation { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public string AccountType { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public string Address { get; set; }
    
        [DataMember(Order = 8)]
        [XmlElement]
        public string Status { get; set; }
    }
}
