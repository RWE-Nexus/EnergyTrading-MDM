namespace EnergyTrading.Mdm
{
    /// <summary>
    /// A system that has entities that we want to map for master data.
    /// </summary>
    public partial class SourceSystem : ISourceSystem
    {
        /// <copydocfrom cref="ISourceSystem.Name" />
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public virtual SourceSystem Parent { get; set; }

        ISourceSystem ISourceSystem.Parent
        {
            get { return Parent; }
        }

        partial void CopyDetails(SourceSystem details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfParent = this.Parent;

            Parent = details.Parent;
            Name = details.Name;
        }
    }
}
