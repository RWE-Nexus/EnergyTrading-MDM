using System.Collections;
using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Data;

    public partial class BrokerRate : IIdentifiable, IEntity
    {
        public BrokerRate()
        {
            this.Mappings = new List<BrokerRateMapping>();
            this.Details = new List<BrokerRateDetails>();
        }

        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual IList<BrokerRateMapping> Mappings { get; private set; }

        public virtual IList<BrokerRateDetails> Details { get; private set; }

        public BrokerRateDetails LatestDetails
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
        public long Version
        {
            get
            {
                var version = this.Details.LatestVersion(this.Mappings.LatestVersion());
                return version;
            }
        }

        /// <summary>
        /// Add a details to the <#= EntityName.ToLower() #> checking its validity 
        /// </summary>
        /// <param name="details"></param>
        public void AddDetails(BrokerRateDetails details)
        {
            if (this.MatchesExistingDetailsPeriod(details))
            {
                this.CopyDetails(details, this.ExistingDetailsPeriod(details));
            }
            else if (this.ShouldUpdateLatestDetail(this.Details, details))
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

        private bool MatchesExistingDetailsPeriod(BrokerRateDetails details)
        {
            return this.Details.Any(x => x.Validity.Start == details.Validity.Start && x.Validity.Finish == details.Validity.Finish);
        }

        private BrokerRateDetails ExistingDetailsPeriod(BrokerRateDetails details)
        {
            return this.Details.SingleOrDefault(x => x.Validity.Start == details.Validity.Start && x.Validity.Finish == details.Validity.Finish);
        }

        /// <summary>
        /// Perform the field by field copy operation
        /// </summary>
        partial void CopyDetails(BrokerRateDetails details);
        
        partial void CopyDetails(BrokerRateDetails source, BrokerRateDetails target);

        /// <summary>
        /// Add or update a mapping, checking that it exists and that the details are compatible.
        /// </summary>
        /// <param name="mapping"></param>
        public void ProcessMapping(BrokerRateMapping mapping)
        {
            this.ProcessMapping(this.Mappings, mapping, this.Validity.Finish);
        }

        void IEntity.AddDetails(IEntityDetail details)
        {
            this.AddDetails(details as BrokerRateDetails);
        }

        void IEntity.ProcessMapping(IEntityMapping mapping)
        {
            this.ProcessMapping(mapping as BrokerRateMapping);
        }
    }
}

