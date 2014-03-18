namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PartyDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2, EmitDefaultValue = false)]
        [XmlElement]
        public string FaxNumber { get; set; }

        [DataMember(Order = 3, EmitDefaultValue = false)]
        [XmlElement]
        public string TelephoneNumber { get; set; }

        [DataMember(Order = 5, Name = "PartyRoleType", EmitDefaultValue = false)]
        [XmlElement(ElementName = "PartyRoleType")]
        public string Role { get; set; }

        [DataMember(Order = 6, EmitDefaultValue = false)]
        [XmlElement]
        public bool IsInternal { get; set; }
    }
}
