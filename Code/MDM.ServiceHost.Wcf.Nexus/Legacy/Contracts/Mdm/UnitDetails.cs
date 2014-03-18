namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Details of a measurement unit
    /// </summary>
    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class UnitDetails
    {
        /// <summary>
        /// Name of the unit
        /// </summary>
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        /// <summary>
        /// Description of the unit
        /// </summary>
        [DataMember(Order = 2)]
        [XmlElement]
        public string Description { get; set; }

        /// <summary>
        /// Symbol used to denote unit, not unique across all units
        /// </summary>
        [DataMember(Order = 3)]
        [XmlElement]
        public string Symbol { get; set; }

        /// <summary>
        /// Conversion factor to express the unit in base SI units
        /// </summary>
        [DataMember(Order = 4)]
        [XmlElement]
        public decimal ConversionFactor { get; set; }

        /// <summary>
        /// Conversion constant to express the unit in base SI units
        /// </summary>
        [DataMember(Order = 5)]
        [XmlElement] 
        public decimal ConversionConstant { get; set; }

        /// <summary>
        /// The dimension we are measuring, can be base SI units, e.g. Time
        /// or a derived dimension e.g. Volume (underlying SI is m^3), Power (kg m^2 s^-3)
        /// </summary>
        [DataMember(Order = 6)]
        [XmlElement]
        public EntityId Dimension { get; set; }
    }
}