namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class TenorDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string ShortName { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId TenorType { get; set; }

        [DataMember(Order = 4, Name = "RelativeIND")]
        [XmlElement(ElementName = "RelativeIND")]
        public bool IsRelative { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string DeliveryRangeType { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public string DeliveryPeriod { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public DateRange Delivery { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public DateRange Traded { get; set; }
    }
}