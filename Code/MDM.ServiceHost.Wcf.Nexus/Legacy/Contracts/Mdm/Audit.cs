namespace RWEST.Nexus.MDM.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class Audit
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public NexusIdList LastChangeUser { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public DateTime LastChangeTimestamp { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public int VersionNumber { get; set; }
    }
}
