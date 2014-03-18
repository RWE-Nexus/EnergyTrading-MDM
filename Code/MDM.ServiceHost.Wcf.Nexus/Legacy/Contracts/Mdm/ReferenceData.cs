namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ReferenceData
    {
        [DataMember(Order = 3, Name = "Value", EmitDefaultValue = false)]
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }

        [DataMember(Order = 4, Name = "ReferenceKey", EmitDefaultValue = false)]
        [XmlElement(ElementName = "ReferenceKey")]
        public string ReferenceKey { get; set; }
    }
}