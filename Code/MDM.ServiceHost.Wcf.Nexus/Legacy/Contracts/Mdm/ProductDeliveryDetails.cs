namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductDeliveryDetails
    {
        [DataMember(Order = 1, EmitDefaultValue = false)]
        [XmlElement]
        public EntityId Product { get; set; }

        [DataMember(Order = 2, EmitDefaultValue = false)]
        [XmlElement]
        public EntityId ProductType { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public string ShortName { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public bool IsRelative { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public string DeliveryPeriod { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public DeliveryDetails DeliveryDetails { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public DateRange Traded { get; set; }
    }
}