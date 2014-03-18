namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductSettlementCurveDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public EntityId Product { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId ProductType { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string InstrumentType { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        // TODO: Is Broker sufficient or just use PartyRole
        public EntityId Broker { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public EntityId SettlementCurve { get; set; }
    }
}