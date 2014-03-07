namespace EnergyTrading.MDM
{
    public partial class BrokerRate
    {
        partial void CopyDetails(BrokerRateDetails details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfBroker = this.LatestDetails.Broker;
            var forceLoadOfDesk = this.LatestDetails.Desk;
            var forceLoadOfCit = this.LatestDetails.CommodityInstrumentType;
            var forceLoadOfLocation = this.LatestDetails.Location;
            var forceLoadOfProductType = this.LatestDetails.ProductType;
            var forceLoadofbrokerrate = this.LatestDetails.BrokerRate;
          

            this.LatestDetails.Validity = this.LatestDetails.Validity.ChangeStart(details.Validity.Start).ChangeFinish(details.Validity.Finish);
            this.LatestDetails.ProductType = details.ProductType;
            this.LatestDetails.CommodityInstrumentType = details.CommodityInstrumentType;
            this.LatestDetails.Location = details.Location;
            this.LatestDetails.ProductType = details.ProductType;
            this.LatestDetails.Rate = details.Rate;
            this.LatestDetails.RateType = details.RateType;
            this.LatestDetails.Currency = details.Currency;
            this.LatestDetails.Broker = details.Broker;
            this.LatestDetails.PartyAction = details.PartyAction;
            this.LatestDetails.BrokerRate = this;
        }

        partial void CopyDetails(BrokerRateDetails source, BrokerRateDetails target)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfBroker = target.Broker;
            var forceLoadOfDesk = target.Desk;
            var forceLoadOfCit = target.CommodityInstrumentType;
            var forceLoadOfLocation = target.Location;
            var forceLoadOfProductType = target.ProductType;
            var forceLoadofbrokerrate = target.BrokerRate;
          

            target.Validity.ChangeStart(source.Validity.Start).ChangeFinish(source.Validity.Finish);
            target.ProductType = source.ProductType;
            target.CommodityInstrumentType = source.CommodityInstrumentType;
            target.Location = source.Location;
            target.ProductType = source.ProductType;
            target.Rate = source.Rate;
            target.RateType = source.RateType;
            target.Currency = source.Currency;
            target.Broker = source.Broker;
            target.PartyAction = source.PartyAction;
            target.BrokerRate = this;
        }
    }
}
