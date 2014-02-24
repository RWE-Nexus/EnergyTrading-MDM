namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading.Data;

    /// <summary>
    /// A mapping from a <see cref="SourceSystem" /> to an MDM entity.
    /// </summary>
    public interface IEntityMapping : IIdentifiable, IRangedChild
    {
        /// <summary>
        /// Gets or sets the MappingId property.
        /// </summary>
        int MappingId { get; set; }

        /// <summary>
        /// Gets or sets the System property.
        /// </summary>
        SourceSystem System { get; set; }

        /// <summary>
        /// Gets or sets the MappingValue property.
        /// </summary>
        string MappingValue { get; set; }

        /// <summary>
        /// Gets or sets the IsMaster property.
        /// <para>
        /// If true this says that this is the golden source of data for the entity.
        /// </para>
        /// </summary>
        bool IsMaster { get; set; }

        /// <summary>
        /// Gets or sets the IsDefault property.
        /// <para>
        /// If true this says that this default mapping for this system for the entity.
        /// </para>
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        /// Change the end date of an existing mapping
        /// </summary>
        /// <param name="value"></param>
        void ChangeEndDate(DateTime value);
    }
}
