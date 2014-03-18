namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public enum PartyAction
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Initiator = 1,
        [EnumMember]
        Aggressor = 2
    }
}