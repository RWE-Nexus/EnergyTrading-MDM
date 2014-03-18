namespace EnergyTrading.MDM
{
    public partial class InstrumentType
    {
        public string Name { get; set; }

        partial void CopyDetails(InstrumentType details)
        {
            this.Name = details.Name;
        }
    }
}