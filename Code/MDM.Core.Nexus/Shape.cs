namespace EnergyTrading.MDM
{
    public partial class Shape
    {
        public string Name { get; set; }

        partial void CopyDetails(Shape details)
        {
            this.Name = details.Name;
        }
    }
}
