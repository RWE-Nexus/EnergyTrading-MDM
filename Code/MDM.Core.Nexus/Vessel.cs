namespace EnergyTrading.MDM
{
    public partial class Vessel
    {

        public string Name { get; set; }
        
        partial void CopyDetails(Vessel details)
        {
            this.Name = details.Name;
        }
    }
}
