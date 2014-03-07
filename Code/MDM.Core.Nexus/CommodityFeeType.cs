namespace EnergyTrading.MDM
{
    public partial class CommodityFeeType
    {
        public virtual Commodity Commodity { get; set; }

        public virtual FeeType FeeType { get; set; }

       // public virtual string InstrumentDelivery { get; set; }

        partial void CopyDetails(CommodityFeeType details)
        {
            var forceLoadOfCommodoity = this.Commodity;
            var forcedLoadOfInstrumentType = this.FeeType;

            //this.InstrumentDelivery = details.InstrumentDelivery;
            this.Commodity = details.Commodity;
            this.FeeType = details.FeeType;
        }
    }
}
