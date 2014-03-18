namespace EnergyTrading.MDM
{
    using System;

    public partial class CommodityInstrumentType
    {
        public virtual Commodity Commodity { get; set; }

        public virtual InstrumentType InstrumentType { get; set; }

        public virtual string InstrumentDelivery { get; set; }

		partial void CopyDetails(CommodityInstrumentType details)
        {
            var forceLoadOfCommodoity = this.Commodity;
            var forcedLoadOfInstrumentType = this.InstrumentType;
            
            this.InstrumentDelivery = details.InstrumentDelivery;
            this.Commodity = details.Commodity;
            this.InstrumentType = details.InstrumentType;
        }
    }
}
