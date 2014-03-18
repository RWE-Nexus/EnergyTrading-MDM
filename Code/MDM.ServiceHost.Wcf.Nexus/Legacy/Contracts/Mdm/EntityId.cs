namespace RWEST.Nexus.MDM.Contracts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using RWEST.Nexus.Contracts.Atom;

    /// <summary>
    /// A foreign key to a <see cref="IMdmEntity" />.
    /// </summary>
    /// <remarks>
    /// Sufficient information so we can display a label and know how to retrieve the target.
    /// </remarks>
    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class EntityId
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EntityId" /> class.
        /// </summary>
        public EntityId()
        {
            this.Links = new List<Link>();
        }

        /// <summary>
        /// Gets or sets the Identifier property.
        /// </summary>
        [DataMember(Order = 1)]
        [XmlElement]
        public NexusId Identifier { get; set; }

        /// <summary>
        /// Gets or sets the name property.
        /// </summary>
        [DataMember(Order = 2)]
        [XmlElement]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Links collection.
        /// <para>
        /// This is the collection of atom links that may provide further information allowing
        /// for a level 3 REST API i.e. hypermedia commands embedded in the data.
        /// </para>
        /// </summary>
        [DataMember(Order = 3, EmitDefaultValue = false)]
        [XmlElement("link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Links { get; set; }

        public override string ToString()
        {
            return this.Identifier.ToString();
        }
    }
}