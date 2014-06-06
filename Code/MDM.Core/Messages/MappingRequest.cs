namespace EnergyTrading.Mdm.Messages
{
    /// <summary>
    /// A request to map from a system to Mdm.
    /// </summary>
    public class MappingRequest : ReadRequest
    {
        /// <summary>
        /// Gets or sets the SystemName property.
        /// <para>
        /// Name of the system we are mapping from.
        /// </para>
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the Identifier property.
        /// <para>
        /// The identifier in the source system.
        /// </para>
        /// </summary>
        public string Identifier { get; set; }
    }
}
