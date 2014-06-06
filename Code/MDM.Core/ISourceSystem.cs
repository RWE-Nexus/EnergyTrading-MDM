namespace EnergyTrading.Mdm
{
    /// <summary>
    /// System providing mappings to MDM.
    /// </summary>
    public interface ISourceSystem
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the parent system.
        /// </summary>
        ISourceSystem Parent { get; }
    }
}