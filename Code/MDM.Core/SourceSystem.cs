namespace EnergyTrading.MDM
{
    /// <summary>
    /// A system that has entities that we want to map for master data.
    /// </summary>
    public partial class SourceSystem 
    {
        public virtual SourceSystem Parent { get; set; }

        public string Name { get; set; }

        partial void CopyDetails(SourceSystem details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfParent = this.Parent;

            Parent = details.Parent;
            Name = details.Name;
        }
    }
}
