namespace RWEST.Nexus.MDM.Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class LegalEntityDetails
    {
        [DataMember(Order = 1)]
        [XmlElement]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public string RegisteredName { get; set; }

        [DataMember(Order = 3)]
        [XmlElement]
        public string RegistrationNumber { get; set; }

        [DataMember(Order = 4)]
        [XmlElement]
        public string Address { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string Website { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public string Email { get; set; }

        [DataMember(Order = 7)]
        [XmlElement]
        public string Fax { get; set; }

        [DataMember(Order = 8)]
        [XmlElement]
        public string Phone { get; set; }

        [DataMember(Order = 9)]
        [XmlElement]
        public string CountryOfIncorporation { get; set; }

        [DataMember(Order = 10)]
        [XmlElement]
        public string PartyStatus { get; set; }

        [DataMember(Order = 11)]
        [XmlElement]
        public string CustomerAddress { get; set; }

        [DataMember(Order = 12)]
        [XmlElement]
        public string InvoiceSetup { get; set; }

        [DataMember(Order = 13)]
        [XmlElement]
        public string VendorAddress { get; set; }
    }
}
