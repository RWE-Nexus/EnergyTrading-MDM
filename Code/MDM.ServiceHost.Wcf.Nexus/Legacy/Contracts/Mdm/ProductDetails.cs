namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Market { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string CalendarRule { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public int LotSize { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public EntityId Shape { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public EntityId CommodityInstrumentType { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        // TODO: Rename to SettlementCurve
        public EntityId DefaultCurve { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public EntityId Exchange { get; set; }

        [DataMember(Order = 9)]
        [XmlElement]
        public string IncoTerms { get; set; }

        [DataMember(Order = 10)]
        [XmlElement]
        public string InstrumentSubType { get; set; }
    }
}