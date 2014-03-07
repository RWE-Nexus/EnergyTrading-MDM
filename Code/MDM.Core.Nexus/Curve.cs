namespace EnergyTrading.MDM
{
    using System;

    public partial class Curve
    {

        public string Name { get; set; }

        public string Type { get; set; }

        public string Currency { get; set; }

        public virtual Commodity Commodity { get; set; }

        public string CommodityUnit { get; set; }

        public virtual Location Location { get; set; }

        public virtual Party Originator { get; set; }

        public decimal DefaultSpread { get; set; }

        partial void CopyDetails(Curve details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfCommodoity = this.Commodity;
            var forceLoadOfLocation = this.Location;
            var forceLoadOfOriginator = this.Originator;

            this.Name = details.Name;
            this.Type = details.Type;
            this.Currency = details.Currency;
            this.Commodity = details.Commodity;
            this.CommodityUnit = details.CommodityUnit;
            this.Location = details.Location;
            this.Originator = details.Originator;
            this.DefaultSpread = details.DefaultSpread;

        }
    }
}
