namespace EnergyTrading.MDM
{
    using EnergyTrading;

    public partial class ProductTypeInstance
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public virtual ProductType ProductType { get; set; }

        public DateRange Delivery { get; set; }

        public string DeliveryPeriod { get; set; }

        public DateRange Traded { get; set; }

        partial void OnCreate()
        {
            Traded = new DateRange();
        }

        partial void CopyDetails(ProductTypeInstance details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfProductType = this.ProductType;

            this.Name = details.Name;
            this.ShortName = details.ShortName;
            this.ProductType = details.ProductType;
            this.Delivery = details.Delivery;
            this.DeliveryPeriod = details.DeliveryPeriod;
            this.Traded = details.Traded;
        }
    }
}