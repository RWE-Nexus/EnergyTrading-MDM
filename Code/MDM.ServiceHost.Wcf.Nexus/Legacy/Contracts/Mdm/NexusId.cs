namespace RWEST.Nexus.MDM.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    [KnownType(typeof(Mapping))]
    public class NexusId : IEquatable<NexusId>
    {
        [DataMember(Order = 1, EmitDefaultValue = false)]
        [XmlElement]
        public Header Header { get; set; }

        [DataMember(Order = 2, Name = "MappingID", EmitDefaultValue = false)]
        [XmlElement(ElementName = "MappingID")]
        public long? MappingId { get; set; }

        [XmlIgnore]
        public bool MappingIdSpecified
        {
            get { return this.MappingId.HasValue; }
        }

        [DataMember(Order = 3, Name = "SystemID", EmitDefaultValue = false)]
        [XmlElement(ElementName = "SystemID")]
        public string SystemId { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public string SystemName { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string Identifier { get; set; }

        [DataMember(Order = 6, Name = "OriginatingSourceIND")]
        [XmlElement(ElementName = "OriginatingSourceIND")]
        public bool SourceSystemOriginated { get; set; }

        [DataMember(Order = 7, Name = "IsNexusID")]
        [XmlElement(ElementName = "IsNexusID")]
        public bool IsNexusId { get; set; }

        [DataMember(Order = 8, Name = "DefaultReverseIND", EmitDefaultValue = false)]
        [XmlElement(ElementName = "DefaultReverseIND")]
        public bool? DefaultReverseInd { get; set; }

        [XmlIgnore]
        public bool DefaultReverseIndSpecified
        {
            get { return this.DefaultReverseInd.HasValue && this.DefaultReverseInd.Value; }
        }

        [DataMember(Order = 9, EmitDefaultValue = false)]
        [XmlElement]
        public DateTime? StartDate { get; set; }

        [XmlIgnore]
        public bool StartDateSpecified
        {
            get { return this.StartDate.HasValue; }
        }

        [DataMember(Order = 10, EmitDefaultValue = false)]
        [XmlElement]
        public DateTime? EndDate { get; set; }

        [XmlIgnore]
        public bool EndDateSpecified
        {
            get { return this.EndDate.HasValue; }
        }

        /// <copydocfrom cref="object.Equals(object)" />
        public override bool Equals(object obj)
        {
            return obj != null && this.Equals(obj as NexusId);
        }

        /// <copydocfrom cref="object.Equals(object)" />
        public bool Equals(NexusId other)
        {
            if (other == null)
            {
                return false;
            }

            // NB Do we need MappingId and Validity?
            return SystemName == other.SystemName && Identifier == other.Identifier;
        }

        /// <copydocfrom cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            return SystemName.GetHashCode() + (Identifier.GetHashCode() * 7);
        }

        /// <copydocfrom cref="object.ToString" />
        public override string ToString()
        {
            return SystemName + "/" + Identifier;
        }
    }
}