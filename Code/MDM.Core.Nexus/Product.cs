namespace EnergyTrading.MDM
{
    using System.Collections.Generic;

    public partial class Product
    {
        public virtual string Name { get; set; }

        public virtual Market Market { get; set; }

        public virtual Exchange Exchange { get; set; }

        public virtual string CalendarRule { get; set; }

        public virtual int LotSize { get; set; }

        public virtual Shape Shape { get; set; }

        public virtual CommodityInstrumentType CommodityInstrumentType { get; set; }

        public virtual Curve DefaultCurve { get; set; }

        public virtual string IncoTerms { get; set; }

        public virtual string InstrumentSubType { get; set; }

        public virtual IList<ProductCurve> ProductCurves { get; set; }

        partial void CopyDetails(Product details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfMarket = this.Market;
            var forceLoadShape = this.Shape;
            var forceLoadCommodityInstrumentType = this.CommodityInstrumentType;
            var forceLoadCurve = this.DefaultCurve;
            var forceLoadExchange = this.Exchange;

            this.Name = details.Name;
            this.Market = details.Market;
            this.Exchange = details.Exchange;
            this.LotSize = details.LotSize;
            this.CalendarRule = details.CalendarRule;
            this.Shape = details.Shape;
            this.CommodityInstrumentType = details.CommodityInstrumentType;
            this.DefaultCurve = details.DefaultCurve;
            this.IncoTerms = details.IncoTerms;
            this.InstrumentSubType = details.InstrumentSubType;
        }
    }
}