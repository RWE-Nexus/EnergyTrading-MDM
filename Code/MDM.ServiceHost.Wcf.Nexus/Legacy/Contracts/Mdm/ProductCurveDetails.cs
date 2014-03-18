namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductCurveDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Product { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId Curve { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public string ProductCurveType { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string ProjectionMethod { get; set; }      
    }
}