namespace EnergyTrading.MDM
{
    using EnergyTrading;

    /// <summary>
    /// An MDM entity
    /// </summary>
    public interface IEntity
    {
        int Id { get; }

        DateRange Validity { get; }

        /// <summary>
        /// Gets the version of the entity.
        /// </summary>
        /// <remarks>
        /// Should test for equality rather than > since entity.Version can be a large negative number
        /// and version will default to 0 if not provided.
        /// </remarks>
        long Version { get; }

        void AddDetails(IEntityDetail details);

        void ProcessMapping(IEntityMapping mapping);
    }
}