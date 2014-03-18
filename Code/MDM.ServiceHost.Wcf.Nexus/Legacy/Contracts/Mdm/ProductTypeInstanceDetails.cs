namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductTypeInstanceDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string ShortName { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId ProductType { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public DateRange Delivery { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string DeliveryPeriod { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public DateRange Traded { get; set; }
    }
}