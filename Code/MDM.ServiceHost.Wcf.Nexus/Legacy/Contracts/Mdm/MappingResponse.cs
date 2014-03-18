namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus", Name = "MappingResponseType")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus", TypeName = "MappingResponseType")]
    public class MappingResponse
    {
        public MappingResponse()
        {
            this.Mappings = new NexusIdList();
        }

        [DataMember(Order = 1)]
        [XmlArray("Mappings")]
        [XmlArrayItem("ReferenceID")]
        public NexusIdList Mappings { get; set; }
    }

    public static class MappingResponseExtensions
    {
        public static MappingResponse ToNexus(this EnergyTrading.Mdm.Contracts.MappingResponse response)
        {
            return new MappingResponse();
        }

        public static EnergyTrading.Mdm.Contracts.MappingResponse FromNexus(this MappingResponse response)
        {
            return new EnergyTrading.Mdm.Contracts.MappingResponse();
        }
    }
}