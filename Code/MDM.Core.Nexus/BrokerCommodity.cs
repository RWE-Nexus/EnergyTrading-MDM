namespace EnergyTrading.MDM
{
    public partial class BrokerCommodity
    {

        public virtual string Name { get; set; }
        public virtual Broker Broker { get; set; }
        public virtual Commodity Commodity { get; set; }

		partial void CopyDetails(BrokerCommodity details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfBroker = this.Broker;
            var forceLoadOfCommodity = this.Commodity;


            this.Name = details.Name;
            this.Broker = details.Broker;
            this.Commodity = details.Commodity;
        }
    }
}
