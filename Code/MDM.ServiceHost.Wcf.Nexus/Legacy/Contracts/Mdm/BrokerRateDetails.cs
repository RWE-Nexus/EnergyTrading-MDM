namespace RWEST.Nexus.MDM.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class BrokerRateDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public EntityId Broker { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Desk { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId CommodityInstrumentType { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId Location { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        [Obsolete("Not required")]
        public EntityId ProductType { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public PartyAction PartyAction { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public decimal Rate { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public string RateType { get; set; }

        [DataMember(Order = 9)]
        [XmlElement]
        public string Currency { get; set; }
    }
}