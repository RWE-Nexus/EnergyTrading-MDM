namespace EnergyTrading.MDM
{
    public partial class LocationRole
    {
        public virtual Location Location { get; set; }

        public virtual LocationRoleType Type { get; set; }

        partial void CopyDetails(LocationRole details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfLocation = this.Location;
            var forceLoadOfLocationRoleType = this.Type;

            this.Location = details.Location;
            this.Type = details.Type;
        }
    }
}