namespace EnergyTrading.MDM
{
    public partial class Market
    {
        public string Name { get; set; }

        public virtual Commodity Commodity { get; set; }

        public virtual Location Location { get; set; }

        public virtual Calendar Calendar { get; set; }

        public string Currency { get; set; }

        public string TradeUnits { get; set; }

        public string TradeUnitsRate { get; set; }

        public string NominationUnits { get; set; }

        public string PriceUnits { get; set; }

        public string DeliveryRate { get; set; }

        public string IncoTerms { get; set; }

        partial void CopyDetails(Market details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfCommodoity = this.Commodity;
            var forceLoadOfLocation = this.Location;
            var forceLoadOfCalendar = this.Calendar;

            this.Name = details.Name;
            this.Commodity = details.Commodity;
            this.Location = details.Location;
            this.Calendar = details.Calendar;
            this.TradeUnits = details.TradeUnits;
            this.TradeUnitsRate = details.TradeUnitsRate;
            this.NominationUnits = details.NominationUnits;
            this.Currency = details.Currency;
            this.PriceUnits = details.PriceUnits;
            this.DeliveryRate = details.DeliveryRate;
            this.IncoTerms = details.IncoTerms;
        }
    }
}