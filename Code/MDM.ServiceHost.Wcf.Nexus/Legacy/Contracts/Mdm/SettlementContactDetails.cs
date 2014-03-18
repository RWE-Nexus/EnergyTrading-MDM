namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class SettlementContactDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId CommodityInstrumentType { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId TargetPerson { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId SourcePerson { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public EntityId TargetParty { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public EntityId SourceParty { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public EntityId Location { get; set; }
    }
}
