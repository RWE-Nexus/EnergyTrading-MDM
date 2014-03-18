namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Details of a dimension, expressed in SI terms
    /// </summary>
    public class DimensionDetails
    {
        /// <summary>
        /// Name of the dimension
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
        /// Exponent for the length dimension
        /// </summary>
        [DataMember(Order = 3)]
        [XmlElement]
        public int LengthDimension { get; set; }

        /// <summary>
        /// Exponent for the mass dimension
        /// </summary>
        [DataMember(Order = 4)]
        [XmlElement]
        public int MassDimension { get; set; }

        /// <summary>
        /// Exponent for the time dimension
        /// </summary>
        [DataMember(Order = 5)]
        [XmlElement]
        public int TimeDimension { get; set; }

        /// <summary>
        /// Exponent for the electric current dimension
        /// </summary>
        [DataMember(Order = 6)]
        [XmlElement]
        public int ElectricCurrentDimension { get; set; }

        /// <summary>
        /// Exponent for the temperature dimension
        /// </summary>
        [DataMember(Order = 7)]
        [XmlElement]
        public int TemperatureDimension { get; set; }

        /// <summary>
        /// Exponent for the luminous intensity dimension
        /// </summary>
        [DataMember(Order = 8)]
        [XmlElement]
        public int LuminousIntensityDimension { get; set; }

        /// <summary>
        /// Exponent for the substance amount dimension
        /// </summary>
        [DataMember(Order = 9)]
        [XmlElement]
        public int SubstanceAmountDimension { get; set; }
    }
}