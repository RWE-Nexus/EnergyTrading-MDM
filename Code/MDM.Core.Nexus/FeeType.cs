namespace EnergyTrading.MDM
{
    public partial class FeeType
    {
        public string Name { get; set; }

        partial void CopyDetails(FeeType details)
        {
            this.Name = details.Name;
        }
    }
}
