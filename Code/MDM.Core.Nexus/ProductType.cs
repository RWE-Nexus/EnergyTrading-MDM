namespace EnergyTrading.MDM
{
    using EnergyTrading;

    public partial class ProductType
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool IsRelative { get; set; }

        public virtual Product Product { get; set; }

        public string DeliveryRangeType { get; set; }

        public string DeliveryPeriod { get; set; }

        public DateRange Traded { get; set; }

        partial void OnCreate()
        {
            Traded = new DateRange();
        }

        partial void CopyDetails(ProductType details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfProduct = this.Product;

            this.Name = details.Name;
            this.ShortName = details.ShortName;
            this.IsRelative = details.IsRelative;
            this.Product = details.Product;
            this.DeliveryRangeType = details.DeliveryRangeType;
            this.DeliveryPeriod = details.DeliveryPeriod;
            this.Traded = details.Traded;
        }
    }
}