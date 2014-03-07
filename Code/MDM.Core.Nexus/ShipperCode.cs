namespace EnergyTrading.MDM
{
    public partial class ShipperCode
    {
        public virtual Location Location { get; set; }

        public virtual Party Party { get; set; }

        public string Code { get; set; }

        partial void CopyDetails(ShipperCode details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfLocation = this.Location;

            Location = details.Location;
            Party = details.Party;
            Code = details.Code;
        }
    }
}