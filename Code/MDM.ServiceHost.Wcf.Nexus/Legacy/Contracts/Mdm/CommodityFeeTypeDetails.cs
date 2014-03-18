namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class CommodityFeeTypeDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public EntityId Commodity { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId FeeType { get; set; }
     }
}
