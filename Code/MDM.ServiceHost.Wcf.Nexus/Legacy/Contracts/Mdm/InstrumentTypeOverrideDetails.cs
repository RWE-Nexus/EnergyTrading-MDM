namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class InstrumentTypeOverrideDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId ProductType { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId ProductTenorType { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId Broker { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public EntityId CommodityInstrumentType { get; set; }
        
        [DataMember(Order = 6)]
        [XmlElement]
        public string InstrumentSubType { get; set; }
    }
}