namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class InstrumentTypeOverrideDetailsChecker : Checker<InstrumentTypeOverrideDetails>
    {
        public InstrumentTypeOverrideDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.ProductType);
            Compare(x => x.Broker);
            Compare(x => x.CommodityInstrumentType);
            Compare(x => x.InstrumentSubType);
        }
    }
}
