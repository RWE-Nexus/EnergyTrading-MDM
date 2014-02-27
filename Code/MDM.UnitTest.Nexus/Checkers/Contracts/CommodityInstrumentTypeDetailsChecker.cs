namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class CommodityInstrumentTypeDetailsChecker : Checker<CommodityInstrumentTypeDetails>
    {
        public CommodityInstrumentTypeDetailsChecker()
        {
            Compare(x => x.Commodity);
            Compare(x => x.InstrumentType);
            Compare(x => x.InstrumentDelivery);
        }
    }
}
