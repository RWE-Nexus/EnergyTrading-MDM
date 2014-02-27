namespace EnergyTrading.MDM
{
    using System;

    public partial class ProductCurve
    {

        public virtual string Name { get; set; }
        public virtual Product Product { get; set; }
        public virtual Curve Curve { get; set; }
        public virtual string ProductCurveType { get; set; }
        public virtual string ProjectionMethod { get; set; }

        partial void CopyDetails(ProductCurve details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfProduct = this.Product;
            var forceLoadOfCurve = this.Curve;

            this.Name = details.Name;
            this.Product = details.Product;
            this.Curve = details.Curve;
            this.ProductCurveType = details.ProductCurveType;
            this.ProjectionMethod = details.ProjectionMethod;
        }
    }
}
