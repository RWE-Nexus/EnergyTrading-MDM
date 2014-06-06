namespace EnergyTrading.Mdm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Extensions;

    /// <summary>
    /// Base implementation of an MDM entity.
    /// </summary>
    public abstract class Entity<TEntity> : IIdentifiable, IEntity, IEntityDetail
        where TEntity : class, IRangedChild
    {
        protected Entity()
        {
            Mappings = new List<IEntityMapping>();
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual IList<IEntityMapping> Mappings { get; private set; }

        IList<IEntityMapping> IEntity.Mappings
        {
            get { return Mappings.ToList(); }
        }

        IEntity IRangedChild.Entity
        {
            get { return this; }
            set { }
        }

        public DateRange Validity { get; set; }

        /// <summary>
        /// Gets or sets the Timestamp property.
        /// <para>
        /// This is implemented by a RowVersion property on the database.
        /// </para>
        /// </summary>
        public byte[] Timestamp { get; set; }

        /// <summary>
        /// Gets the version property.
        /// <para>
        /// This is the Timestamp property converted to a long, only equality operations
        /// should be performed with this value as relative difference has no business meaning.
        /// </para>
        /// </summary>
        public ulong Version
        {
            get
            {
                var version = this.Timestamp.ToUnsignedLongVersion();
                version = this.Mappings.LatestVersion(version);
                return version;
            }
        }

        /// <summary>
        /// Add a details to the entity checking its validity 
        /// </summary>
        /// <param name="details"></param>
        public void AddDetails(TEntity details)
        {
            // Sanity checks
            if (details == null)
            {
                throw new ArgumentNullException("details");
            }

            // Copy the bits across
            CopyDetails(details);
            this.Validity = details.Validity;

            // Trim all the mappings that extend past the end of the entity.
            this.Mappings.TrimMappings(this.Validity.Finish);
        }

        /// <summary>
        /// Add or update a mapping, checking that it exists and that the details are compatible.
        /// </summary>
        /// <param name="mapping"></param>
        public void ProcessMapping(IEntityMapping mapping)
        {
            this.ProcessMapping(this.Mappings, mapping, this.Validity.Finish);
        }

        void IEntity.AddDetails(IEntityDetail details)
        {
            this.AddDetails(details as TEntity);
        }

        /// <summary>
        /// Perform the field by field copy operation
        /// </summary>
        protected abstract void CopyDetails(TEntity details);

        /// <summary>
        /// Allow for construction actions in the partial class.
        /// </summary>
        protected virtual void OnCreate()
        {            
        }
    }
}
