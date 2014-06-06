namespace EnergyTrading.Mdm
{
    using EnergyTrading.Mdm.Data;

    /// <summary>
    /// Child supporting validity range and versioning.
    /// </summary>
    public interface IRangedChild : IRanged
    {
        /// <summary>
        /// Get or set the entity.
        /// </summary>
        IEntity Entity { get; set; }

        /// <summary>
        /// Get the version.
        /// </summary>
        ulong Version { get; }
    }
}