namespace EnergyTrading.MDM
{
    public partial class ShapeDay
    {
        public virtual string DayType { get; set; }

        public virtual Shape Shape { get; set; }

        public virtual ShapeElement ShapeElement { get; set; }

        partial void CopyDetails(ShapeDay details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfShape = this.Shape;
            var forceLoadOfShapeElement = this.ShapeElement;
            
            this.DayType = details.DayType;
            this.Shape = details.Shape;
            this.ShapeElement = details.ShapeElement;
        }
    }
}
