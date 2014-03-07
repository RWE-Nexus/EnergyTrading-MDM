namespace EnergyTrading.MDM
{
    using System.Collections.Generic;

    using EnergyTrading;
    using EnergyTrading.Data;

    public class PartyRole : IIdentifiable, IEntity
    {
        public PartyRole()
        {
            this.Mappings = new List<PartyRoleMapping>();
            this.Details = new List<PartyRoleDetails>();
        }

        public int Id { get; set; }

        public virtual Party Party { get; set; }

        public virtual string PartyRoleType { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual IList<PartyRoleMapping> Mappings { get; private set; }

        public virtual IList<PartyRoleDetails> Details { get; private set; }

        public PartyRoleDetails LatestDetails
        {
            get { return this.Details.Latest(); }
        }

        public DateRange Validity
        {
            get
            {
                // TODO: Should span from earliest to latest to get the range of the party as an entity
                var latest = this.LatestDetails;
                return latest != null ? latest.Validity : DateRange.MaxDateRange;
            }
        }

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
                var version = this.Details.LatestVersion(this.Mappings.LatestVersion());
                return version;
            }
        }

        /// <summary>
        /// Add a details to the party checking its validity 
        /// </summary>
        /// <param name="details"></param>
        public virtual void AddDetails(PartyRoleDetails details)
        {
            if (this.ShouldUpdateLatestDetail(this.Details, details))
            {
                this.CopyDetails(details);
            }
            else
            {
                this.AddDetails(this.Details, details);
            }

            // Trim all the mappings that extend past the end of the entity.
            this.Mappings.TrimMappings(this.Validity.Finish);
        }

        /// <summary>
        /// Add or update a mapping, checking that it exists and that the details are compatible.
        /// </summary>
        /// <param name="mapping"></param>
        public void ProcessMapping(PartyRoleMapping mapping)
        {
            this.ProcessMapping(this.Mappings, mapping, this.Validity.Finish);     
        }

        void IEntity.AddDetails(IEntityDetail details)
        {
            this.AddDetails(details as PartyRoleDetails);
        }

        void IEntity.ProcessMapping(IEntityMapping mapping)
        {
            this.ProcessMapping(mapping as PartyRoleMapping);
        }

        protected virtual void CopyDetails(PartyRoleDetails details)
        {
            // force the load of related entiies to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfPartyRole = this.LatestDetails.PartyRole;

            this.LatestDetails.Name = details.Name;
            this.LatestDetails.Validity = this.LatestDetails.Validity.ChangeStart(details.Validity.Start).ChangeFinish(details.Validity.Finish);
            this.LatestDetails.PartyRole = this;

            CopyAdditionalDetails(details);
        }

        protected virtual void CopyAdditionalDetails(PartyRoleDetails details)
        {
            //Implement in child classes
        }
    }
}