namespace EnergyTrading.MDM
{
    public partial class Location 
    {
        public virtual Location Parent { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        partial void CopyDetails(Location details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfParent = this.Parent;

            this.Parent = details.Parent;
            this.Name = details.Name;
            this.Type = details.Type;
        }
    }
}