namespace EnergyTrading.MDM.Data
{
    using EnergyTrading;

    /// <summary>
    /// Supports a validity range.
    /// </summary>
    public interface IRanged
    {
        /// <summary>
        /// Get or set the validity range.
        /// </summary>
        DateRange Validity { get; set; }
    }
}