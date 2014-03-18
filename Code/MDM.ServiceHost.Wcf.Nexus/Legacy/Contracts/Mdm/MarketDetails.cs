namespace RWEST.Nexus.MDM.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class MarketDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Location { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId Calendar { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId Commodity { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string Currency { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public string TradeUnits { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public string TradeUnitsRate { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public string NominationUnits { get; set; }

        [DataMember(Order = 9)]
        [XmlElement]
        public string PriceUnits { get; set; }

        [DataMember(Order = 10)]
        [XmlElement]
        public string DeliveryRate { get; set; }

        [DataMember(Order = 11)]
        [XmlElement]
        public string IncoTerms { get; set; }
    }
}