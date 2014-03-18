namespace EnergyTrading.MDM
{
    using System;

    public partial class InstrumentTypeOverride
    {
        public virtual string Name { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Broker Broker { get; set; }
        public virtual CommodityInstrumentType CommodityInstrumentType { get; set; }
        public virtual string InstrumentSubType { get; set; }
        public virtual ProductTenorType ProductTenorType { get; set; }

        partial void CopyDetails(InstrumentTypeOverride details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfProductType = this.ProductType;
            var forceLoadOfBroker = this.Broker;
            var forceLoadOfCommodityInstrumentType = this.CommodityInstrumentType;
            var forceLoadOfProductTenorType = this.ProductTenorType;

            this.Name = details.Name;
            this.ProductType = details.ProductType;
            this.Broker = details.Broker;
            this.CommodityInstrumentType = details.CommodityInstrumentType;
            this.InstrumentSubType = details.InstrumentSubType;
            this.ProductTenorType = details.ProductTenorType;
        }
    }
}
