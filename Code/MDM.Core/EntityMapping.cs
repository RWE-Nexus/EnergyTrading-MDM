namespace EnergyTrading.Mdm
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Extensions;

    /// <summary>
    /// Generic mapping for a <see cref="SourceSystem" /> to an MDM entity.
    /// </summary>
    public abstract class EntityMapping : IEntityMapping
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EntityMapping"/> calss.
        /// </summary>
        protected EntityMapping()
        {
            this.Validity = new DateRange();
            this.Version = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        int IEntityMapping.MappingId
        {
            get { return this.Id; }
            set { this.Id = value; }
        }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        protected abstract IEntity Entity { get; set; }

        /// <copydocfrom cref="IRangedChild.Entity" />
        IEntity IRangedChild.Entity
        {
            get { return this.Entity; }
            set { this.Entity = value; }
        }

        /// <summary>
        /// Gets or sets the System
        /// </summary>
        public virtual SourceSystem System { get; set; }

        ISourceSystem IEntityMapping.System
        {
            get { return System; }
            set { System = (SourceSystem) value; }
        }

        /// <copydocfrom cref="IEntityMapping.MappingValue" />
        public string MappingValue { get; set; }

        /// <copydocfrom cref="IEntityMapping.IsMaster" />
        public bool IsMaster { get; set; }

        /// <copydocfrom cref="IEntityMapping.IsDefault" />
        public bool IsDefault { get; set; }

        /// <copydocfrom cref="IEntityMapping.Validity" />
        public DateRange Validity { get; set; }

        public byte[] Version { get; set; }

        ulong IRangedChild.Version
        {
            get { return this.Version.ToUnsignedLongVersion(); }
        }

        public void ChangeStartDate(DateTime value)
        {
            var earliest = this.Entity.Validity.Start;

            if (earliest > value)
            {
                // Trim to start of entity lifespan
                value = earliest;
            }

            // Update the validity start date
            this.Validity = this.Validity.ChangeStart(value);
        }

        /// <summary>
        /// Change the end date of a mapping.
        /// <para>
        /// Enforces the constraint that the mapping cannot continue after the end of the person.
        /// </para>
        /// </summary>
        /// <param name="value">DateTime to change to</param>
        /// <exception cref="NotSupportedException">If the person has expired</exception>
        public void ChangeEndDate(DateTime value)
        {
            var latest = Entity.Validity.Finish;
            if (latest < SystemTime.UtcNow())
            {
                throw new NotSupportedException("Cannot change mapping for expired entity");               
            }

            if (latest < value)
            {
                // Trim to end of entity lifespan
                value = latest;
            }

            // Update the validity end date
            this.Validity = this.Validity.ChangeFinish(value);
        }
    }
}