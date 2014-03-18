namespace RWEST.Nexus.MDM.Contracts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using RWEST.Nexus.Contracts.Atom;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PartyAccountability : IMdmEntity
    {
        public PartyAccountability()
        {
            this.Identifiers = new NexusIdList();
            this.Details = new PartyAccountabilityDetails();
            this.Links = new List<Link>();
        }

        [DataMember(Order = 1)]
        [XmlArray]
        [XmlArrayItem("ReferenceID")]
        public NexusIdList Identifiers { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public PartyAccountabilityDetails Details { get; set; }
       
        [DataMember(Order = 8, EmitDefaultValue = false)]
        [XmlElement]
        public SystemData Nexus { get; set; }

        [DataMember(Order = 9, EmitDefaultValue = false)]
        [XmlElement]
        public Audit Audit { get; set; }

        [DataMember(Order = 10, EmitDefaultValue = false)]
        [XmlElement("link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Links { get; set; }
        
        object IMdmEntity.Details
        {
            get { return this.Details; }
            set { this.Details = (PartyAccountabilityDetails)value; }
        }
    }
}