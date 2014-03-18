namespace RWEST.Nexus.MDM.Contracts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [CollectionDataContract(Namespace = "http://schemas.rwe.com/nexus", ItemName = "CalendarDay")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class CalendarDayList : List<CalendarDay>
    {
    }
}