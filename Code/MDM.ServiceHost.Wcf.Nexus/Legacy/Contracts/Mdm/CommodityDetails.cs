namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class CommodityDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public EntityId Parent { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public decimal? MassEnergyValue { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public EntityId MassEnergyUnits { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public decimal? VolumeEnergyValue { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public EntityId VolumeEnergyUnits { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public decimal? VolumetricDensityValue { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public EntityId VolumetricDensityUnits { get; set; }

        [DataMember(Order = 9)]
        [XmlElement]
        public string DeliveryRate { get; set; }
    }
}