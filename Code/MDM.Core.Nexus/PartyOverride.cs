namespace EnergyTrading.MDM
{
    public partial class PartyOverride
    {
        public virtual Broker Broker { get; set; }
        public virtual CommodityInstrumentType CommodityInstrumentType { get; set; }
        public virtual string MappingValue { get; set; }
        public virtual Party Party { get; set; }
        
        partial void CopyDetails(PartyOverride details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfBroker = this.Broker;
            var forceLoadOfCommodityInstrumentType = this.CommodityInstrumentType;
            var forceLoadOfParty = this.Party;

            this.Broker = details.Broker;
            this.CommodityInstrumentType = details.CommodityInstrumentType;
            this.MappingValue = details.MappingValue;
            this.Party = details.Party;
        }
    }    
}
