namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Type of days that can be specified in a calendar.
    /// </summary>
    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public enum DayType
    {
        [EnumMember]
        Working,
        [EnumMember]
        Weekend,
        [EnumMember]
        Holiday,
        [EnumMember]
        Long,
        [EnumMember]
        Short
    }
}