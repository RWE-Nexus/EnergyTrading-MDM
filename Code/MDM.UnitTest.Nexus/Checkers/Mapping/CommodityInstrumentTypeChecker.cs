namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CommodityInstrumentTypeChecker : Checker<CommodityInstrumentType>
    {
        public CommodityInstrumentTypeChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Commodity).Id();
            Compare(x => x.InstrumentType).Id();
            Compare(x => x.InstrumentDelivery);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
