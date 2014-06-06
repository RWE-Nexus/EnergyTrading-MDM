namespace EnergyTrading.Mdm.Messages
{
    /// <summary>
    /// A request to map from one system to another.
    /// </summary>
    public class CrossMappingRequest : MappingRequest
    {
        /// <summary>
        /// Gets or sets the TargetSystemName property.
        /// <para>
        /// Name of the system whose identifier we are requesting.
        /// </para>
        /// </summary>
        public string TargetSystemName { get; set; }
    }
}