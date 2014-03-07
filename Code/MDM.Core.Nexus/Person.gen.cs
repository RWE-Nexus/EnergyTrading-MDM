namespace EnergyTrading.MDM
{
    using System.Collections.Generic;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public partial class Person : IIdentifiable, IEntity
    {
        public Person()
        {
            this.Mappings = new List<PersonMapping>();
            this.Details = new List<PersonDetails>();
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual IList<PersonMapping> Mappings { get; private set; }

        public virtual IList<PersonDetails> Details { get; private set; }

        public PersonDetails LatestDetails
        {
            get { return this.Details.Latest(); }
        }

        public DateRange Validity
        {
            get { return this.Details.GetEntityValidity(); }
        }

        /// <remarks>
        /// Should test for equality rather than > since entity.Version can be a large negative number
        /// and version will default to 0 if not provided.
        /// </remarks>
        public ulong Version
        {
            get
            {
                var version = this.Details.LatestVersion(this.Mappings.LatestVersion());
                return version;
            }
        }

        /// <summary>
        /// Add a details to the person checking its validity 
        /// </summary>
        /// <param name="details"></param>
        public void AddDetails(PersonDetails details)
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
        partial void CopyDetails(PersonDetails details);

        /// <summary>
        /// Add or update a mapping, checking that it exists and that the details are compatible.
        /// </summary>
        /// <param name="mapping"></param>
        public void ProcessMapping(PersonMapping mapping)
        {
            this.ProcessMapping(this.Mappings, mapping, this.Validity.Finish);
        }

        void IEntity.AddDetails(IEntityDetail details)
        {
            this.AddDetails(details as PersonDetails);
        }

        void IEntity.ProcessMapping(IEntityMapping mapping)
        {
            this.ProcessMapping(mapping as PersonMapping);
        }
    }
}