namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class CurveDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string CurveType { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string Currency { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId Commodity { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string CommodityUnit { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public EntityId Location { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public EntityId Originator { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public decimal DefaultSpread { get; set; }
    }
}
