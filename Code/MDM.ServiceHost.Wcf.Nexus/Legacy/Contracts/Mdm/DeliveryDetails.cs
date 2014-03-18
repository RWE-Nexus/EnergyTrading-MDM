namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class DeliveryDetails
    {
        [DataMember(Order = 2)]
        [XmlElement]
        public string DeliveryRule { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string DeliveryRuleStart { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public string DeliveryRuleFinish { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public DateRange Period { get; set; }
    }
}