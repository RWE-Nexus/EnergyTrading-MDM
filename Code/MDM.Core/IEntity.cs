namespace EnergyTrading.Mdm
{
    using System.Collections.Generic;

    using EnergyTrading;

    /// <summary>
    /// An MDM entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the entity's mappings.
        /// </summary>
        IList<IEntityMapping> Mappings { get; }

        /// <summary>
        /// Get the validity range of the entity.
        /// </summary>
        DateRange Validity { get; }

        /// <summary>
        /// Gets the version of the entity.
        /// </summary>
        ulong Version { get; }

        /// <summary>
        /// Add a details to the entity checking its validity 
        /// </summary>
        /// <param name="details">Details to add</param>
        void AddDetails(IEntityDetail details);

        /// <summary>
        /// Add or update a mapping, checking that it exists and that the details are compatible.
        /// </summary>
        /// <param name="mapping">Mapping to process</param>
        void ProcessMapping(IEntityMapping mapping);
    }
}