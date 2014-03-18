namespace RWEST.Nexus.MDM.Contracts
{
    using System.Collections.Generic;

    using RWEST.Nexus.Contracts.Atom;

    /// <summary>
    /// Interface supported by all MDM entities.
    /// </summary>
    public interface IMdmEntity
    {
        /// <summary>
        /// Gets or sets the Identifiers property.
        /// </summary>
        NexusIdList Identifiers { get; set; }

        /// <summary>
        /// Gets or sets the details property.
        /// <para>
        /// The actual type will vary by MDM entity, this gives a polymorphic way of accessing the details.
        /// </para>
        /// </summary>
        object Details { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        SystemData Nexus { get; set; }

        /// <summary>
        /// Gets or sets the Audit property.
        /// </summary>
        Audit Audit { get; set; }

        /// <summary>
        /// Gets or sets the Links collection.
        /// <para>
        /// This is the collection of atom links that may provide further information allowing
        /// for a level 3 REST API i.e. hypermedia commands embedded in the data.
        /// </para>
        /// </summary>
        List<Link> Links { get; set; }
    }

//    public static class LegacyIMdmEntityExtensions
//    {
//        public static IMdmEntity ToNexus(this EnergyTrading.Mdm.Contracts.IMdmEntity mdmEntity)
//        {
//            
//        }
//    }
}