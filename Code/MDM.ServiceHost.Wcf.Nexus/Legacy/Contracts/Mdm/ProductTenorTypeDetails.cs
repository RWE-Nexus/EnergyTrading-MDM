namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductTenorTypeDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public EntityId Product { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId TenorType { get; set; }
    }
}