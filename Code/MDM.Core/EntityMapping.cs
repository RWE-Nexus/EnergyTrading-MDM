namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Data;

    /// <summary>
    /// Generic mapping from a <see cref="SourceSystem" /> to an MDM entity.
    /// </summary>
    public abstract class EntityMapping : IEntityMapping
    {
        protected EntityMapping()
        {
            this.Validity = new DateRange();
            this.Version = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

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

        protected abstract IEntity Entity { get; set; }

        IEntity IRangedChild.Entity 
        {
            get { return this.Entity; }
            set { this.Entity = value; }
        }

        public virtual SourceSystem System { get; set; }

        public string MappingValue { get; set; }

        public bool IsMaster { get; set; }

        public bool IsDefault { get; set; }

        public DateRange Validity { get; set; }

        public byte[] Version { get; set; }

        long IRangedChild.Version
        {
            get { return BitConverter.ToInt64(this.Version, 0); }
        }

        /// <summary>
        /// Updates a mapping from another mapping
        /// </summary>
        /// <param name="value"></param>
        public void UpdateMapping(EntityMapping value)
        {
            if (!this.CompatibleMapping(value))
            {
                throw new ArgumentOutOfRangeException("value", "Mapping not compatible");    
            }

            this.ChangeStartDate(value.Validity.Start);
            this.ChangeEndDate(value.Validity.Finish);
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

        public bool CompatibleMapping(EntityMapping candidate)
        {
            return this.Id == candidate.Id 
                && this.System == candidate.System 
                && this.MappingValue == candidate.MappingValue 
                && this.IsMaster == candidate.IsMaster 
                && this.IsDefault == candidate.IsDefault;
        }
    }
}