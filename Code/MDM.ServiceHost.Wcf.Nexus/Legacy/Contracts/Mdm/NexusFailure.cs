namespace RWEST.Nexus.MDM.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using RWEST.Nexus.Contracts.Atom;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus", Name = "NexusFailureType")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus", TypeName = "NexusFailureType")]
    public class NexusFailure
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Message { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string Reason { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string SourceSystem { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public string Mapping { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public DateTime? AsOfDate { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public string Identifier { get; set; }

        [DataMember(Order = 7, EmitDefaultValue = false)]
        [XmlElement]
        public Link Link { get; set; }
    }
}