namespace EnergyTrading.MDM
{
    using System;

    using EnergyTrading.Data;

    public class SettlementContact : PartyAccountability, IIdentifiable, IEntity
    {
        public virtual CommodityInstrumentType CommodityInstrumentType { get; set; }

        public virtual Location Location { get; set; }

        protected override void CopyAdditionalDetails(PartyAccountability details)
        {
            var settlementContactDetails = (SettlementContact)details;
            var forceLoadCommodityInstrumentType = this.CommodityInstrumentType;
            var forceLoadLocation = this.Location;

            this.CommodityInstrumentType = settlementContactDetails.CommodityInstrumentType;
            this.Location = settlementContactDetails.Location;
        }
    }
}

