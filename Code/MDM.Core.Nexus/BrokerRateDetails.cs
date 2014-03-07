namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public class BrokerRateDetails : IIdentifiable, IEntityDetail
    {
        public BrokerRateDetails()
        {
            this.Validity = new DateRange();
            this.Timestamp = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public int Id { get; set; }

        public virtual int PartyAction { get; set; }

        public virtual decimal Rate { get; set; }
        
        public virtual string RateType { get; set; }
        
        public virtual string Currency { get; set; }

        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public virtual BrokerRate BrokerRate { get; set; }

        public virtual Broker Broker { get; set; }

        public virtual CommodityInstrumentType CommodityInstrumentType { get; set; }

        public virtual Location Location { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual PartyRole Desk { get; set; }


        public IEntity Entity
        {
            get { return this.BrokerRate; }
            set { this.BrokerRate = value as BrokerRate; }
        }
        //public IEntity broker
        //{
        //    get { return this.Broker; }
        //    set { this.Broker = value as Broker; }
        //}
       
        // public IEntity location
        //{
        //    get { return this.Location; }
        //    set { this.Location = value as Location; }
        //}

        // public IEntity producttype
        //{
        //    get { return this.ProductType; }
        //    set { this.ProductType = value as ProductType; }
        //}

        //public IEntity commodityinstrumenttype
        //{
        //    get { return this.CommodityInstrumentType; }
        //    set { this.CommodityInstrumentType = value as CommodityInstrumentType; }
        //}

        //public IEntity partyrole
        //{
        //    get { return this.PartyRole; }
        //    set { this.PartyRole = value as PartyRole; }
        //}
 
        public DateRange Validity { get; set; }

        public byte[] Timestamp { get; set; }

        public ulong Version
        {
            get { return this.Timestamp.ToUnsignedLongVersion(); }
        }
    }
}

