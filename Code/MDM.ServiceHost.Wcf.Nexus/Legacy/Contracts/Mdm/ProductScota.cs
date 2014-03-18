namespace RWEST.Nexus.MDM.Contracts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using RWEST.Nexus.Contracts.Atom;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class ProductScota : IMdmEntity
    {
        public ProductScota()
        {
            this.Identifiers = new NexusIdList();
            this.Details = new ProductScotaDetails();
            this.Links = new List<Link>();
        }

        [DataMember(Order = 1)]
        [XmlArray]
        [XmlArrayItem("ReferenceID")]
        public NexusIdList Identifiers { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public ProductScotaDetails Details { get; set; }

        [DataMember(Order = 3, EmitDefaultValue = false)]
        [XmlElement]
        public SystemData Nexus { get; set; }

        [DataMember(Order = 4, EmitDefaultValue = false)]
        [XmlElement]
        public Audit Audit { get; set; }

        [DataMember(Order = 5, EmitDefaultValue = false)]
        [XmlElement("link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Links { get; set; }

        object IMdmEntity.Details
        {
            get { return this.Details; }
            set { this.Details = (ProductScotaDetails)value; }
        }
    }
}