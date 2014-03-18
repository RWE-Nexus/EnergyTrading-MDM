namespace EnergyTrading.MDM
{
    public partial class TenorType
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        //public bool IsRelative { get; set; }

        //public string DeliveryRangeType { get; set; }

        //public string DeliveryPeriod { get; set; }

        //public DateRange Traded { get; set; }

        partial void CopyDetails(TenorType details)
        {
            this.Name = details.Name;
            this.ShortName = details.ShortName;
            //this.IsRelative = details.IsRelative;
            //this.DeliveryRangeType = details.DeliveryRangeType;
            //this.DeliveryPeriod = details.DeliveryPeriod;
            //this.Traded = details.Traded;
        }
    }
}
