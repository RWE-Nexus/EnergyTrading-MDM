namespace EnergyTrading.MDM
{
    using EnergyTrading;

    public partial class ShapeElement
    {
        public virtual string Name { get; set; }

        public virtual DateRange Period { get; set; }

        partial void CopyDetails(ShapeElement details)
        {
            this.Name = details.Name;
            this.Period = details.Period;
        }
    }
}
