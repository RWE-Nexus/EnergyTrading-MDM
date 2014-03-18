namespace RWEST.Nexus.MDM.Contracts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ShapeDayDetails
    {
        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Shape { get; set; }

        [DataMember(Order = 1)]
        [XmlElement]
        public string DayType { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId ShapeElement { get; set; }
    }
}