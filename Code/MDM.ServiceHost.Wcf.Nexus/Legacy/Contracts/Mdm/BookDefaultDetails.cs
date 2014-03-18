namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class BookDefaultDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public virtual EntityId Trader { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public virtual EntityId Desk { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public virtual string GfProductMapping { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public virtual EntityId Book { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public virtual string DefaultType { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public virtual EntityId PartyRole { get; set; }
    }
}