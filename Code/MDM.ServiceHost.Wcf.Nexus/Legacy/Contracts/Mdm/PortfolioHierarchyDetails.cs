namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PortfolioHierarchyDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public EntityId ChildPortfolio { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId ParentPortfolio { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public EntityId Hierarchy { get; set; }
    }
}
