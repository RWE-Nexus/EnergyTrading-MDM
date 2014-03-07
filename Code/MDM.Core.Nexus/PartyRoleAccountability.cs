namespace EnergyTrading.MDM
{
    using System;
    using System.Collections.Generic;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleAccountability : IIdentifiable, IEntity, IEntityDetail
    {
        public PartyRoleAccountability()
        {
            this.Mappings = new List<PartyRoleAccountabilityMapping>();
            this.Validity = new DateRange();
            this.Timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual IList<PartyRoleAccountabilityMapping> Mappings { get; private set; }

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

        public virtual string PartyRoleAccountabilityType { get; set; }

        public virtual PartyRole SourcePartyRole { get; set; }

        public virtual PartyRole TargetPartyRole { get; set; }

        public virtual string Name { get; set; }

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
        /// Add a details to the PartyRoleAccountability checking its validity 
        /// </summary>
        /// <param name="details"></param>
        public void AddDetails(PartyRoleAccountability details)
        {
            // Sanity checks
            if (details == null)
            {
                throw new ArgumentNullException("details");
            }

            // Copy the bits across
            CopyDetails(details);
            CopyAdditionalDetails(details);
            this.Validity = details.Validity;

            // Trim all the mappings that extend past the end of the entity.
            this.Mappings.TrimMappings(this.Validity.Finish);
        }

        protected virtual void CopyAdditionalDetails(PartyRoleAccountability details)
        {
        }

        /// <summary>
        /// Perform the field by field copy operation
        /// </summary>
        private void CopyDetails(PartyRoleAccountability details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadSourceParty = this.SourcePartyRole;
            var forceLoadTargetParty = this.TargetPartyRole;

            this.Name = details.Name;
            this.PartyRoleAccountabilityType = details.PartyRoleAccountabilityType;
            this.SourcePartyRole = details.SourcePartyRole;
            this.TargetPartyRole = details.TargetPartyRole;
        }

        /// <summary>
        /// Add or update a mapping, checking that it exists and that the details are compatible.
        /// </summary>
        /// <param name="mapping"></param>
        public void ProcessMapping(PartyRoleAccountabilityMapping mapping)
        {
            this.ProcessMapping(this.Mappings, mapping, this.Validity.Finish);
        }

        void IEntity.AddDetails(IEntityDetail details)
        {
            this.AddDetails(details as PartyRoleAccountability);
        }

        void IEntity.ProcessMapping(IEntityMapping mapping)
        {
            this.ProcessMapping(mapping as PartyRoleAccountabilityMapping);
        }
    }
}