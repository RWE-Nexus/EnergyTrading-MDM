namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PersonDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Forename { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string Surname { get; set; }

        [DataMember(Order = 3, EmitDefaultValue = false)]
        [XmlElement]
        public string FaxNumber { get; set; }

        [DataMember(Order = 4, EmitDefaultValue = false)]
        [XmlElement]
        public string TelephoneNumber { get; set; }

        [DataMember(Order = 5, Name = "PersonRoleType", EmitDefaultValue = false)]
        [XmlElement(ElementName = "PersonRoleType")]
        public string Role { get; set; }

        [DataMember(Order = 6, EmitDefaultValue = false)]
        public string Email { get; set; }
    }
}