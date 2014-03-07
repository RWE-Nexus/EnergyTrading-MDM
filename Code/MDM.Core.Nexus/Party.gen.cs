namespace EnergyTrading.MDM
{
    using System.Collections.Generic;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public partial class Party : IIdentifiable, IEntity
    {
        public Party()
        {
            this.Mappings = new List<PartyMapping>();
            this.Details = new List<PartyDetails>();
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual IList<PartyMapping> Mappings { get; private set; }

        public virtual IList<PartyDetails> Details { get; private set; }

        public PartyDetails LatestDetails
        {
            get { return this.Details.Latest(); }
        }

        public DateRange Validity
        {
            get { return this.Details.GetEntityValidity(); }
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
        public void AddDetails(PartyDetails details)
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
        /// Perform the field by field copy operation
        /// </summary>
        partial void CopyDetails(PartyDetails details);

        /// <summary>
        /// Add or update a mapping, checking that it exists and that the details are compatible.
        /// </summary>
        /// <param name="mapping"></param>
        public void ProcessMapping(PartyMapping mapping)
        {
            this.ProcessMapping(this.Mappings, mapping, this.Validity.Finish);
        }

        void IEntity.AddDetails(IEntityDetail details)
        {
            this.AddDetails(details as PartyDetails);
        }

        void IEntity.ProcessMapping(IEntityMapping mapping)
        {
            this.ProcessMapping(mapping as PartyMapping);
        }
    }
}