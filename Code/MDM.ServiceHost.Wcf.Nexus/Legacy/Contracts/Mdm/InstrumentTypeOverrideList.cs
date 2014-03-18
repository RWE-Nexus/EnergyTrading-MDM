using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace RWEST.Nexus.MDM.Contracts
{
    [CollectionDataContract(Namespace = "http://schemas.rwe.com/nexus", ItemName = "InstrumentTypeOverride")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class InstrumentTypeOverrideList : List<InstrumentTypeOverride>
    {
    }
}