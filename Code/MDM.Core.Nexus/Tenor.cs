namespace EnergyTrading.MDM
{
    using EnergyTrading;

    public partial class Tenor
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public virtual TenorType TenorType { get; set; }

        public bool IsRelative { get; set; }

        public string DeliveryPeriod { get; set; }

        public string DeliveryRangeType { get; set; }

        public DateRange Delivery { get; set; }

        public DateRange Traded { get; set; }

        partial void CopyDetails(Tenor details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfTenorType = this.TenorType;

            this.Name = details.Name;
            this.ShortName = details.ShortName;
            this.TenorType = details.TenorType;
            this.IsRelative = details.IsRelative;
            this.DeliveryPeriod = details.DeliveryPeriod;
            this.DeliveryRangeType = details.DeliveryRangeType;
            this.Delivery = details.Delivery;
            this.Traded = details.Traded;
        }
    }
}
