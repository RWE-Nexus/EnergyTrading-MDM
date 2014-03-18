namespace EnergyTrading.MDM
{
    using System.Collections.Generic;

    /// <summary>
    /// Party represents companies, business units and trading desks, but not person.
    /// </summary>
    public partial class Party
    {
        public virtual IList<PartyRole> PartyRoles { get; set; }

        partial void CopyDetails(PartyDetails details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfParty = this.LatestDetails.Party;

            this.LatestDetails.Name = details.Name;
            this.LatestDetails.Fax = details.Fax;
            this.LatestDetails.Party = details.Party;
            this.LatestDetails.Phone = details.Phone;
            this.LatestDetails.Role = details.Role;
            this.LatestDetails.IsInternal = details.IsInternal;
            this.LatestDetails.Validity = this.LatestDetails.Validity.ChangeStart(details.Validity.Start).ChangeFinish(details.Validity.Finish);
            this.LatestDetails.Party = this;
        }
    }
}