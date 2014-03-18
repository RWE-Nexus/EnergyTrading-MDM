namespace RWEST.Nexus.MDM.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class CalendarDay
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public DateTime CalendarDate { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public DayType CalendarDayType { get; set; }
    }
}